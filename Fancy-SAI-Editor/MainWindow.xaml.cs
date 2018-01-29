using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;

namespace NodeAI
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes the MainWindow and the database and addes the MenuItems to the node selection menu.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            //Set the focus to the object to be able to handle key events
            NodeEditorCanvas.Focus();

            nodeStore = new List<Node>();
            selectedNode = null;
            cachedNode = null;

            Database.InitializeDatabase("SERVER=" + Properties.Settings.Default.MysqlServer + "; " +
                "DATABASE=" + Properties.Settings.Default.MysqlWorldDatabase + ";" +
                "UID=" + Properties.Settings.Default.MysqlUsername + ";" +
                " PASSWORD=" + Properties.Settings.Default.MysqlPassword,
                "Data Source=" + Properties.Settings.Default.SQLiteDatabase + "; Version=3;");

            //Create Menu items for Nodes
            Assembly assembly = Assembly.GetExecutingAssembly();
            foreach(var type in assembly.GetTypes())
            {
                if (type.GetCustomAttribute<NodeAttribute>() != null)
                {
                    NodeMenuItem menuItem = new NodeMenuItem(type.GetCustomAttribute<NodeAttribute>().Type)
                    {
                        Header = type.GetCustomAttribute<NodeAttribute>().MenuName,
                    };
                    menuItem.Click += HandleNodeMenu;

                    if (type.IsSubclassOf(typeof(Nodes.ActionNodes.ActionNode)))
                    {
                        actionNode.Items.Add(menuItem);
                    }
                    else if (type.IsSubclassOf(typeof(Nodes.EventNodes.EventNode)))
                    {
                        eventNode.Items.Add(menuItem);
                    }
                    else if (type.IsSubclassOf(typeof(Nodes.GeneralNodes.GeneralNode)))
                    {
                        generalNode.Items.Add(menuItem);
                    }
                }
            }
        }

        private List<Node> nodeStore;
        private Node selectedNode;
        private Node cachedNode;

        private void HandleNodeMenu(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is NodeMenuItem item)
                {
                    Node newNode = CreateNode(item.Type, item.CreatePosition);
                    UpdateLayout();
                    if (item.Origin != null)
                    {
                        newNode.ConnectToNode(item.Origin);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error at node creation!");
            }
        }

        /// <summary>
        /// Creates node with passed type and returns this node if the creation was successfull.
        /// </summary>
        /// <param name="nodeType">Type of the node to be created.</param>
        public Node CreateNode(NodeType nodeType, Point createPosition = default(Point))
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                foreach (var type in assembly.GetTypes())
                {
                    if (type.GetCustomAttribute<NodeAttribute>() != null && type.GetCustomAttribute<NodeAttribute>().Type == nodeType)
                    {
                        if(Activator.CreateInstance(type) is Node newNode)
                        {
                            nodeStore.Add(newNode);
                            Canvas.SetLeft(newNode, createPosition.X);
                            Canvas.SetTop(newNode, createPosition.Y);
                            NodeEditorCanvas.Children.Add(newNode);

                            //Force calculation of actual width and height
                            NodeEditorCanvas.UpdateLayout();

                            Random random = new Random();
                            while(CheckNodeCollision(newNode))
                            {
                                Canvas.SetLeft(newNode, Math.Abs(random.NextDouble() * NodeEditorCanvas.ActualWidth - newNode.ActualWidth));
                                Canvas.SetTop(newNode, Math.Abs(random.NextDouble() * NodeEditorCanvas.ActualHeight - newNode.ActualHeight));
                            }

                            return newNode;
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error at node creation!");
            }
            return null;
        }

        /// <summary>
        /// Shows a node selection menu in the editor at the mouse position with all node which has a connection possibilities with the passed NodeConnector.
        /// </summary>
        /// <param name="connector"></param>
        public void ShowNodeSelectionMenuInNodeEditor(NodeConnector connector)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            foreach (var type in assembly.GetTypes())
            {
                if (type.GetCustomAttribute<NodeAttribute>() == null || type.GetCustomAttribute<NodeAttribute>().AllowedTypes == null)
                    continue;

                if (connector.CanConnect(type.GetCustomAttribute<NodeAttribute>().Type) && (type.GetCustomAttribute<NodeAttribute>().AllowedTypes.Contains(connector.ParentNodeType) || type.GetCustomAttribute<NodeAttribute>().AllowedTypes.Contains(Node.GetSuperiorType(connector.ParentNodeType))))
                {
                    NodeMenuItem menuItem = new NodeMenuItem(type.GetCustomAttribute<NodeAttribute>().Type, connector.ParentNode)
                    {
                        Header = type.GetCustomAttribute<NodeAttribute>().MenuName,
                    };
                    menuItem.Click += HandleNodeMenu;

                    if (type.Namespace.Contains("GeneralNodes"))
                        selectionMenuGeneralNodes.Items.Add(menuItem);
                    else if (type.Namespace.Contains("ActionNodes"))
                        selectionMenuActionNodes.Items.Add(menuItem);
                    else if (type.Namespace.Contains("EventNodes"))
                        selectionMenuEventNodes.Items.Add(menuItem);
                    else if (type.Namespace.Contains("TargetNodes"))
                        selectionMenuTargetNodes.Items.Add(menuItem);
                }
            }
        }

        /// <summary>
        /// Shows a node selection menu in the editor at the mouse position which allows to create any node at the mouse position
        /// </summary>
        /// <param name="connector"></param>
        public void ShowNodeSelectionMenuInNodeEditor(Point mousePosition)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            foreach (var type in assembly.GetTypes())
            {
                if (type.GetCustomAttribute<NodeAttribute>() == null)
                    continue;

                NodeMenuItem menuItem = new NodeMenuItem(type.GetCustomAttribute<NodeAttribute>().Type, mousePosition)
                {
                    Header = type.GetCustomAttribute<NodeAttribute>().MenuName,
                };
                menuItem.Click += HandleNodeMenu;
                if (type.Namespace.Contains("GeneralNodes"))
                    selectionMenuGeneralNodes.Items.Add(menuItem);
                else if (type.Namespace.Contains("ActionNodes"))
                    selectionMenuActionNodes.Items.Add(menuItem);
                else if (type.Namespace.Contains("EventNodes"))
                    selectionMenuEventNodes.Items.Add(menuItem);
                else if (type.Namespace.Contains("TargetNodes"))
                    selectionMenuTargetNodes.Items.Add(menuItem);
            }
        }

        #region Node Connection Handling

        private List<Tuple<FrameworkElement, FrameworkElement, Path>> nodeConnectionStore = new List<Tuple<FrameworkElement, FrameworkElement, Path>>();

        /// <summary>
        /// Adds a connection displayed by a cubic bezier spline between the two NodeConnectors.
        /// </summary>
        /// <param name="c1">First NodeConnector</param>
        /// <param name="c2">Second NodeConnector</param>
        public void AddNodeConnectionVisual(NodeConnector c1, NodeConnector c2)
        {
            try
            {
                Ellipse e1 = c1.ellipse;
                Ellipse e2 = c2.ellipse;

                Path connection = new Path
                {
                    Stroke = Brushes.LightBlue,
                };

                Point p1 = new Point(e1.TransformToAncestor(NodeEditorCanvas).Transform(new Point(0, 0)).X + e1.ActualWidth / 2, (e1.TransformToAncestor(NodeEditorCanvas).Transform(new Point(0, 0)).Y + e1.ActualHeight / 2));
                Point p2 = new Point(e2.TransformToAncestor(NodeEditorCanvas).Transform(new Point(0, 0)).X + e2.ActualWidth / 2, (e2.TransformToAncestor(NodeEditorCanvas).Transform(new Point(0, 0)).Y + e2.ActualHeight / 2));

                BezierSegment spline = new BezierSegment()
                {
                    Point1 = new Point(p1.X + (p2.X - p1.X) * 0.66, p1.Y),
                    Point2 = new Point(p2.X + (p1.X - p2.X) * 0.66, p2.Y),
                    Point3 = p2,
                };

                PathFigure pathFigure = new PathFigure
                {
                    StartPoint = p1
                };
                pathFigure.Segments.Add(spline);

                PathGeometry connectionGeometry = new PathGeometry();
                connectionGeometry.Figures.Add(pathFigure);

                connection.Data = connectionGeometry;

                nodeConnectionStore.Add(new Tuple<FrameworkElement, FrameworkElement, Path>(e1, e2, connection));
                NodeEditorCanvas.Children.Add(connection);
            }
            catch (Exception e)
            {
                MessageBox.Show("Unknown error!\n" + e.Message);
            }
        }

        /// <summary>
        /// Updates the node connection lines position.
        /// </summary>
        private void UpdateNodeConnections()
        {
            foreach(Tuple<FrameworkElement, FrameworkElement, Path> nodeConnection in nodeConnectionStore)
            {
                Point p1 = new Point(nodeConnection.Item1.TransformToAncestor(NodeEditorCanvas).Transform(new Point(0, 0)).X + nodeConnection.Item1.ActualWidth / 2, (nodeConnection.Item1.TransformToAncestor(NodeEditorCanvas).Transform(new Point(0, 0)).Y + nodeConnection.Item1.ActualHeight / 2));
                Point p2 = new Point(nodeConnection.Item2.TransformToAncestor(NodeEditorCanvas).Transform(new Point(0, 0)).X + nodeConnection.Item2.ActualWidth / 2, (nodeConnection.Item2.TransformToAncestor(NodeEditorCanvas).Transform(new Point(0, 0)).Y + nodeConnection.Item2.ActualHeight / 2));

                BezierSegment spline = new BezierSegment()
                {
                    Point1 = new Point(p1.X + (p2.X - p1.X) * 0.66, p1.Y),
                    Point2 = new Point(p2.X + (p1.X - p2.X) * 0.66, p2.Y),
                    Point3 = p2,
                };

                PathFigure pathFigure = new PathFigure
                {
                    StartPoint = p1
                };
                pathFigure.Segments.Add(spline);

                PathGeometry connectionGeometry = new PathGeometry();
                connectionGeometry.Figures.Add(pathFigure);

                nodeConnection.Item3.Data = connectionGeometry;
            }
        }

        #endregion

        private void ExportToSql(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog
            {
                FileName = "SAI", // Default file name
                DefaultExt = ".sql", // Default file extension
                Filter = "SQL File (.sql)|*.sql" // Filter files by extension
            };

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                SqlExporter exporter = new SqlExporter(nodeStore);
                exporter.Export(dlg.FileName);
            }
        }

        #region Selecting, pasting and deleting handling

        /// <summary>
        /// Selects the passed Node and adds a border to visualze which node is selected.
        /// </summary>
        /// <param name="node">To be selected node.</param>
        public void SelectNode(Node node)
        {
            if(selectedNode != null)
                selectedNode.NodeSelectionBorder.BorderThickness = new Thickness(0);
            selectedNode = node;
            selectedNode.NodeSelectionBorder.BorderThickness = new Thickness(1);
        }

        private void DeselectNode(object sender, MouseButtonEventArgs e)
        {
            if (selectedNode != null)
            {
                selectedNode.NodeSelectionBorder.BorderThickness = new Thickness(0);
                selectedNode = null;
            }
        }

        private void HandleKeyboard(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Delete:
                    if (selectedNode != null)
                    {
                        nodeStore.Remove(selectedNode);
                        NodeEditorCanvas.Children.Remove(selectedNode);
                        selectedNode = null;
                    }
                    break;
                case Key.C:
                    if ((System.Windows.Input.Keyboard.IsKeyDown(Key.LeftCtrl) || System.Windows.Input.Keyboard.IsKeyDown(Key.RightCtrl)) && selectedNode != null)
                        cachedNode = selectedNode;
                    break;
                case Key.V:
                    if ((System.Windows.Input.Keyboard.IsKeyDown(Key.LeftCtrl) || System.Windows.Input.Keyboard.IsKeyDown(Key.RightCtrl)) && cachedNode != null)
                    {
                        Node newNode = cachedNode.Clone();
                        nodeStore.Add(newNode);
                        NodeEditorCanvas.Children.Add(newNode);
                    }
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Positioning

        private void HandlePositionNodes(object sender, RoutedEventArgs e)
        {
            /*
             * The positioning system works like this:
             * 1. Search all NPC nodes which should has the SAI
             * 2. Call PositionRight for every node connected to an output connector:
             *      -> Position all nodes right of the NPC node
             *      -> Call PositionRight for every to an output connector connected node
             *      -> Call PositionLeft for every to an input connector connected node which isn't the reference node(Important because otherwise there would be an endless loop!)
             */
            foreach (Node node in nodeStore)
            {
                if (node.Type == NodeType.GENERAL_NPC && node is Nodes.GeneralNodes.Npc npcNode && npcNode.GetDirectlyConnectedNodes(NodeType.EVENT).Count != 0)
                {
                    List<Node> rightNodes = new List<Node>();
                    double y = Canvas.GetTop(node);
                    foreach (Node rightNode in node.GetDirectlyConnectedNodes(NodeType.NONE, NodeConnectorType.OUTPUT))
                    {
                        PositionRight(rightNode, node);
                        rightNodes.Add(rightNode);
                        y += rightNode.ActualHeight;
                    }
                    y -= (y - Canvas.GetTop(node)) / 2 + node.ActualHeight / 2;

                    y += rightNodes.First().ActualHeight + 5;
                    foreach (Node rightNode in rightNodes)
                    {
                        y -= rightNode.ActualHeight;
                        Canvas.SetTop(rightNode, y);
                        y -= 5;
                    }
                }
            }
        }

        private void PositionRight(Node node, Node referenceNode)
        {
            double newPosition = Canvas.GetLeft(referenceNode) + referenceNode.ActualWidth + 50;
            Canvas.SetLeft(node, newPosition);
            foreach (Node rightNode in node.GetDirectlyConnectedNodes(NodeType.NONE, NodeConnectorType.OUTPUT))
                PositionRight(rightNode, node);

            //Also check if there is any node on the left hand side which isn't the reference node
            foreach (Node leftNode in node.GetDirectlyConnectedNodes(NodeType.NONE, NodeConnectorType.INPUT))
            {
                if(leftNode != referenceNode)
                    PositionLeft(leftNode, node);
            }
        }

        private void PositionLeft(Node node, Node referenceNode)
        {
            double newPosition = Canvas.GetLeft(referenceNode) - node.ActualWidth - 50;
            Canvas.SetLeft(node, newPosition);
            foreach (Node leftNode in node.GetDirectlyConnectedNodes(NodeType.NONE, NodeConnectorType.INPUT))
            {
                PositionLeft(leftNode, node);
            }
        }

        private int GetTopologicalDepth(Node node)
        {
            int depth = 1;

            List<Node> connectedNodes = node.GetDirectlyConnectedNodes(NodeType.NONE, NodeConnectorType.OUTPUT);
            foreach(Node connectedNode in connectedNodes)
            {
                int newDepth = GetTopologicalDepth(connectedNode);
                if (newDepth >= depth)
                    return newDepth + depth;
            }

            return depth;
        }

        #endregion

        /// <summary>
        /// Checks if the passed node collides with any other node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="nodePosition">Optional parameter to pass a node position that is different to the actual visible position of the node.</param>
        /// <returns></returns>
        public bool CheckNodeCollision(Node node, Rect nodePosition = new Rect())
        {
            if (nodePosition.Size == new Size(0,0))
                nodePosition = new Rect(Canvas.GetLeft(node), Canvas.GetTop(node), node.ActualWidth, node.ActualHeight);
            foreach(Node checkNode in nodeStore)
            {
                if (checkNode == node)
                    continue;
                Rect checkNodePosition = new Rect(Canvas.GetLeft(checkNode), Canvas.GetTop(checkNode), checkNode.ActualWidth, checkNode.ActualHeight);
                if (nodePosition.IntersectsWith(checkNodePosition))
                    return true;
            }

            return false;
        }

        private void HandleContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            //Prevent opening of an empty node selection context menu somewhere in the canvas
            if(selectionMenuTargetNodes.Items.IsEmpty && selectionMenuActionNodes.Items.IsEmpty && selectionMenuEventNodes.Items.IsEmpty && selectionMenuGeneralNodes.Items.IsEmpty)
                e.Handled = true;
        }

        private void HandleContextMenuClosing(object sender, ContextMenuEventArgs e)
        {
            //Clear the content of the node selection context menu to prevent wrong items at next opening
            selectionMenuTargetNodes.Items.Clear();
            selectionMenuActionNodes.Items.Clear();
            selectionMenuEventNodes.Items.Clear();
            selectionMenuGeneralNodes.Items.Clear();
        }

        private void NodeEditorCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            double ScaleRate = 1.1;
            if (e.Delta > 0)
            {
                NodeEditorCanvasScaleTransfrom.ScaleX *= ScaleRate;
                NodeEditorCanvasScaleTransfrom.ScaleY *= ScaleRate;
            }
            else
            {
                NodeEditorCanvasScaleTransfrom.ScaleX /= ScaleRate;
                NodeEditorCanvasScaleTransfrom.ScaleY /= ScaleRate;
            }
        }

        private void HandleRightClick(object sender, MouseButtonEventArgs e)
        {
            //Show node creation menu
            if(e.Source is Canvas)
                ShowNodeSelectionMenuInNodeEditor(e.GetPosition(null));
        }

        private void Window_LayoutUpdated(object sender, EventArgs e)
        {
            UpdateNodeConnections();
        }
    }

    
}
