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

            //Initialize node manager
            NodeManager.Instance = new NodeManager(NodeEditorCanvas, NodeEditorCanvasScaleTransfrom);

            Database.InitializeDatabase();

            CreateNodeCreationMenu();
        }

        private void CreateNodeCreationMenu()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            foreach (var type in assembly.GetTypes())
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
                            NodeManager.Instance.AddNode(newNode);
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

        private void HandleKeyboard(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Delete:
                    NodeManager.Instance.DeleteNodes();
                    break;
                case Key.C:
                    NodeManager.Instance.CopyNodes();
                    break;
                case Key.V:
                    NodeManager.Instance.PasteNodes();
                    break;
                default:
                    break;
            }
        }

        private void HandlePositionNodes(object sender, RoutedEventArgs e)
        {
            NodeManager.Instance.Position();
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
            NodeManager.Instance.Scale(e.Delta);
        }

        private void HandleRightClick(object sender, MouseButtonEventArgs e)
        {
            //Show node creation menu
            if(e.Source is Canvas)
                ShowNodeSelectionMenuInNodeEditor(e.GetPosition(null));
        }

        private void Window_LayoutUpdated(object sender, EventArgs e)
        {
            NodeManager.Instance.Update();
        }

        private Point dragAnchorPoint;

        private void NodeEditorCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if(Mouse.LeftButton == MouseButtonState.Pressed && e.OriginalSource == NodeEditorCanvas)
            {
                Mouse.OverrideCursor = Cursors.ScrollAll;

                Point curPoint = e.GetPosition(null);

                double offsetX = dragAnchorPoint.X - curPoint.X;
                double offsetY = dragAnchorPoint.Y - curPoint.Y;

                //Check if offset is greater than minimum offset
                //This must be done before scaling the offset because this config value
                //should be independent of the panning ratio
                if (Math.Abs(offsetX) < Properties.Settings.Default.PanningMinOffset)
                    offsetX = 0;
                if (Math.Abs(offsetY) < Properties.Settings.Default.PanningMinOffset)
                    offsetY = 0;

                //Scale offset to right value
                offsetX = NodeEditorScrollViewer.ContentHorizontalOffset + offsetX * Properties.Settings.Default.PanningRatio;
                offsetY = NodeEditorScrollViewer.ContentVerticalOffset + offsetY * Properties.Settings.Default.PanningRatio;

                //Check if the horizontal offset exceeds the actual scrollable content
                if (offsetX > NodeEditorScrollViewer.ScrollableWidth)
                {
                    //Enlarge editor canvas horizontally
                    NodeEditorCanvas.Width = NodeEditorCanvas.ActualWidth + offsetX / Properties.Settings.Default.PanningRatio - NodeEditorScrollViewer.ContentHorizontalOffset;
                    UpdateLayout();
                }
                //Check if the vertival offset exceeds the actual scrollable content
                if (offsetY > NodeEditorScrollViewer.ScrollableHeight)
                {
                    //Enlarge editor canvas vertically
                    NodeEditorCanvas.Height = NodeEditorCanvas.ActualHeight + offsetY / Properties.Settings.Default.PanningRatio - NodeEditorScrollViewer.ContentVerticalOffset;
                    UpdateLayout();
                }

                //TODO: Diminish canvas size if there is no content in the part outside the screen

                NodeEditorScrollViewer.ScrollToHorizontalOffset(offsetX);
                NodeEditorScrollViewer.ScrollToVerticalOffset(offsetY);

                dragAnchorPoint = curPoint;
            }
        }

        private void NodeEditorCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NodeManager.Instance.DeselectNodes();

            dragAnchorPoint = e.GetPosition(null);
        }

        private void NodeEditorCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //Reset cursor
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void NodeEditorScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            //Route this event manually because I didn't figured out how to prevent this yet
            NodeEditorCanvas_MouseWheel(sender, e);
            e.Handled = true;
        }

        private void HandleMenu_ExportToSql(object sender, RoutedEventArgs e)
        {
            NodeManager.Instance.Export();
        }
    }
}
