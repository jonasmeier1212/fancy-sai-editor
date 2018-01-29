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
using System.Data;
using System.Windows.Controls.Primitives;

namespace NodeAI
{
    /// <summary>
    /// Interaktionslogik für Node.xaml
    /// </summary>
    public abstract partial class Node : UserControl
    {
        /// <summary>
        /// Initializes the Node.
        /// </summary>
        public Node()
        {
            InitializeComponent();
            LoadTooltip();
            connectorStore = new List<NodeConnector>();
            paramStore = new Dictionary<ParamId, Func<string>>();
        }

        /// <summary>
        /// Declaration of abstract method.
        /// </summary>
        /// <returns></returns>
        public abstract Node Clone();

        /// <summary>
        /// Loads the appropriate tooltip from the database and adds it to the node.
        /// </summary>
        private async void LoadTooltip()
        {
            string tooltip = await Database.FindNodeTooltip(type);
            ToolTip toolTip = new ToolTip()
            {
                Content = tooltip,
            };

            NodeSelectionBorder.ToolTip = toolTip;
        }

        #region Fields & Attributes
        private MainWindow mainWindow;
        private NodeType type;
        private List<NodeConnector> connectorStore;
        private NodeData nodeData;
        private Dictionary<ParamId, Func<string>> paramStore; //The Func object stores the method to get the value of this param

        /// <summary>
        /// Attribute to get, set the node type.
        /// </summary>
        public NodeType Type { get => type; set => type = value; }
        /// <summary>
        /// Attribute to get the MainWindow.
        /// </summary>
        public MainWindow MainWindow
        {
            get
            {
                if (mainWindow == null)
                {
                    DependencyObject parent = this.Parent;
                    while (!(parent is MainWindow))
                    {
                        parent = LogicalTreeHelper.GetParent(parent);
                    }
                    mainWindow = parent as MainWindow;
                }
                return mainWindow;
            }
        }

        /// <summary>
        /// Stores the data of the Node. Mostly loaded from the database. Prefer usage of an instance of a derivation class from DataTable in DataStructures.cs
        /// </summary>
        public NodeData NodeData { get => nodeData; set => nodeData = value; }

        #endregion

        /// <summary>
        /// Connects the node to the passed node.
        /// </summary>
        /// <param name="node">Node to connect to.</param>
        public void ConnectToNode(Node node)
        {
            NodeConnector originNodeConnector = null;
            NodeConnector targetNodeConnector = null;
            foreach (NodeConnector co in connectorStore)
            {
                foreach(NodeConnector ct in node.connectorStore)
                {
                    if (co.CanConnect(ct) && ct.CanConnect(co))
                    {
                        originNodeConnector = co;
                        targetNodeConnector = ct;
                    }
                }
            }

            if (originNodeConnector == null || targetNodeConnector == null)
                return;

            MainWindow.AddNodeConnectionVisual(originNodeConnector, targetNodeConnector);
            originNodeConnector.ConnectTo(targetNodeConnector);
            targetNodeConnector.ConnectTo(originNodeConnector);
        }

        #region Node Layout

        #region Params
        /// <summary>
        /// Adds param as text input
        /// </summary>
        public void AddParam(ParamId id, string description, bool allowNaN = false)
        {
            Label label = new Label()
            {
                Content = description,
                Foreground = Brushes.White,
            };
            Grid.SetColumn(label, 0);
            Grid.SetRow(label, paramGrid.Rows);

            TextBox input = new TextBox();
            if (!allowNaN)
            {
                input.Text = "0";
                input.PreviewTextInput += CheckInputForNaN;
            }
            Grid.SetColumn(input, 1);
            Grid.SetRow(input, paramGrid.Rows);

            paramGrid.Children.Add(label);
            paramGrid.Children.Add(input);

            paramStore.Add(id, new Func<string>(() =>
            {
                return input.Text;
            }));
        }

        /// <summary>
        /// Adds param as selection from enum
        /// </summary>
        public void AddParam<T>(ParamId id, string selectionName) where T : struct, IConvertible
        {
            paramGrid.Rows++;
            ComboBox selection = new ComboBox()
            {
                MaxWidth = 100,
            };

            Label name = new Label()
            {
                Foreground = Brushes.White,
                Content = selectionName,
                HorizontalAlignment = HorizontalAlignment.Center,
            };
            Grid.SetColumn(name, 0);
            Grid.SetRow(name, paramGrid.Rows);
            paramGrid.Children.Add(name);

            foreach (var flag in Enum.GetNames(typeof(T)))
            {
                ComboBoxItem boxItem = new ComboBoxItem()
                {
                    Content = flag,
                };
                Grid.SetColumn(boxItem, 1);
                Grid.SetRow(name, paramGrid.Rows);
                selection.Items.Add(boxItem);
            }

            paramGrid.Children.Add(selection);

            if (!selection.Items.IsEmpty)
                (selection.Items[0] as ComboBoxItem).IsSelected = true;

            paramStore.Add(id, new Func<string>(() =>
            {
                
                return ((int)Enum.Parse(typeof(T), (selection.SelectedValue as ComboBoxItem).Content.ToString())).ToString();
            }));
        }

        /// <summary>
        /// Adds param as connected node
        /// </summary>
        public void AddParam<T>(ParamId id, NodeType type, string description) where T : Nodes.GeneralNodes.GeneralNode
        {
            AddInputConnector(description, type);
            paramStore.Add(id, new Func<string>(() => {
                Node connectedNode = GetDirectlyConnectedNode(type);
                if (connectedNode != null && connectedNode is Nodes.GeneralNodes.GeneralNode generalNode)
                {
                    return generalNode.GetParamValue();
                }
                return "0";
            }));
        }

        public string GetParam(ParamId id)
        {
            if (paramStore.ContainsKey(id))
                return paramStore[id].Invoke();

            return "0";
        }

        private void CheckInputForNaN(object sender, TextCompositionEventArgs e)
        {
            try
            {
                Convert.ToInt32(e.Text);
            }
            catch (FormatException)
            {
                e.Handled = true;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Unknown error!\nError: " + exc.Message);
            }
        }
        #endregion

        #region Connectors
        /// <summary>
        /// Adds an input connector for the node.
        /// </summary>
        /// <param name="label">Description text of the connector.</param>
        /// <param name="allowedNode">Type of nodes allowed for connection.</param>
        protected void AddInputConnector(string label, NodeType allowedNode, int nmbAllowedConnections = 1)
        {
            AddConnector(NodeConnectorType.INPUT, label, allowedNode, nmbAllowedConnections);
        }

        /// <summary>
        /// Adds an output connector for the node.
        /// </summary>
        /// <param name="label">Description text of the connector.</param>
        /// <param name="allowedNode">Type of nodes allowed for connection.</param>
        protected void AddOutputConnector(string label, NodeType allowedNode, int nmbAllowedConnections = 1)
        {
            AddConnector(NodeConnectorType.OUTPUT, label, allowedNode, nmbAllowedConnections);
        }

        private void AddConnector(NodeConnectorType type, string label, NodeType allowedNode, int nmbAllowedConnections)
        {
            NodeConnector newConnector = new NodeConnector(label, type, this, allowedNode, nmbAllowedConnections);
            if (type == NodeConnectorType.INPUT)
                inputNodesPanel.Children.Add(newConnector);
            else
                outputNodesPanel.Children.Add(newConnector);
            connectorStore.Add(newConnector);
        }
        #endregion

        #endregion

        #region Drag and Selection Handling

        private TranslateTransform transform = new TranslateTransform();
        private Point anchorPoint;
        private Point currentPoint;

        private void OnLeftMouseDown(object sender, MouseButtonEventArgs e)
        {
            anchorPoint = new Point(System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y);

            MainWindow.SelectNode(this);

            e.Handled = true;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            //Prevent movement if the mouse is not pressed
            if (Mouse.LeftButton == MouseButtonState.Pressed && !(e.Source is TextBox || e.Source is ComboBox || e.Source is ComboBoxItem))
            {
                currentPoint = new Point(System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y);

                System.Windows.Forms.Cursor.Clip = new System.Drawing.Rectangle();

                Canvas.SetLeft(this, Canvas.GetLeft(this) + (currentPoint.X - anchorPoint.X) * (2 - MainWindow.NodeEditorCanvasScaleTransfrom.ScaleX));
                Canvas.SetTop(this, Canvas.GetTop(this) + (currentPoint.Y - anchorPoint.Y) * (2 - MainWindow.NodeEditorCanvasScaleTransfrom.ScaleY));

                MainWindow.UpdateNodeConnections();

                anchorPoint = currentPoint;
            }
            else if(Mouse.LeftButton != MouseButtonState.Pressed && System.Windows.Forms.Cursor.Clip.X != 0 && System.Windows.Forms.Cursor.Clip.Y != 0)
            {
                System.Windows.Forms.Cursor.Clip = new System.Drawing.Rectangle();
            }
        }

        #endregion

        #region Helper Functions

        /// <summary>
        /// Gets the superior type of the node.
        /// If you call this for example in NodeNpc you will get NodeType.GENERAL.
        /// </summary>
        /// <returns>Returns superior node type.</returns>
        public NodeType GetSuperiorType()
        {
            return GetSuperiorType(this.Type);
        }

        /// <summary>
        /// Gets the superior type of the node. Returns NodeType.NONE if the passed NodeType is already a superior type.
        /// If you call this for example with NodeType.GENERAL_NPC you will get NodeType.GENERAL.
        /// </summary>
        /// <param name="type">Type of node to be checked.</param>
        /// <returns>Returns superior node type. Returns NodeType.NONE if the passed NodeType is already a superior type.</returns>
        public static NodeType GetSuperiorType(NodeType type)
        {
            if (type == NodeType.NONE)
                return NodeType.NONE;
            if (type < NodeType.EVENT_MAX && type != NodeType.EVENT)
                return NodeType.EVENT;
            if (type > NodeType.EVENT_MAX && type < NodeType.ACTION_MAX && type != NodeType.ACTION)
                return NodeType.ACTION;
            if (type > NodeType.ACTION_MAX && type < NodeType.GENERAL_MAX && type != NodeType.GENERAL)
                return NodeType.GENERAL;
            if (type > NodeType.GENERAL_MAX && type < NodeType.TARGET_MAX && type != NodeType.TARGET)
                return NodeType.TARGET;
            return NodeType.NONE;
        }

        /// <summary>
        /// Searchs all connected nodes with passed type and puts them in a list.
        /// </summary>
        /// <returns>
        /// Returns a list filled with all connected nodes.
        /// </returns>
        public List<Node> GetConnectedNodes(NodeType type = NodeType.NONE, NodeConnectorType connectorType = NodeConnectorType.NONE)
        {
            List<Node> nodeList = new List<Node>();
            GetConnectedNodes(nodeList, NodeType.NONE, type, connectorType);
            return nodeList;
        }

        private void GetConnectedNodes(List<Node> nodeList, NodeType originType, NodeType type, NodeConnectorType connectorType = NodeConnectorType.NONE)
        {
            foreach(NodeConnector connector in connectorStore)
            {
                if (connectorType != NodeConnectorType.NONE && connector.Type != connectorType)
                    continue;
                foreach (NodeConnector connection in connector.ConnectedNodeConnectors)
                {
                    if (connection.ParentNode == null)
                        continue;

                    if (connection.ParentNode.type != originType)
                    {
                        if(connection.ParentNode.Type == type || GetSuperiorType(connection.ParentNode.Type) == type || type == NodeType.NONE)
                            nodeList.Add(connection.ParentNode);
                        connection.ParentNode.GetConnectedNodes(nodeList, this.type, type);
                    }
                }
            }
        }

        /// <summary>
        /// Gets a to this node connected node with passed type.
        /// The node can be connected over several corners.
        /// </summary>
        /// <param name="type">Type of the node to be searched.</param>
        /// <returns>Return the node of the passed type. If there is no such node it returns null.</returns>
        public Node GetConnectedNode(NodeType type)
        {
            return GetConnectedNode(type, NodeType.NONE);
        }

        private Node GetConnectedNode(NodeType nodeType, NodeType origin)
        {
            foreach (NodeConnector connector in connectorStore)
            {
                foreach (NodeConnector connection in connector.ConnectedNodeConnectors)
                {
                    if (connection.ParentNode == null)
                        continue;

                    if (connection.ParentNode.type == nodeType)
                        return connection.ParentNode;

                    if (connection.ParentNode.type != origin)
                        return connection.ParentNode.GetConnectedNode(nodeType, this.type);
                }
            }
            return null;
        }

        /// <summary>
        /// Gets a to this node directly connected node with passed type.
        /// </summary>
        /// <param name="type">Type of the node to be searched.</param>
        /// <returns>Return the node of the passed type. If there is no such node it returns null.</returns>
        public Node GetDirectlyConnectedNode(NodeType type)
        {
            NodeConnector connector = FindNodeConnectorWithAllowedType(type);

            if (connector == null)
                return null;

            if(connector.AllowedConnectionCount > 1)
            {
                MessageBox.Show("Connector can have more than one node connected!");
                return null;
            }

            if (connector.ConnectedNodeConnectors.Count > 0)
                return connector.ConnectedNodeConnectors.First().ParentNode;

            return null;
        }

        public List<Node> GetDirectlyConnectedNodes(NodeType type, NodeConnectorType connectorType = NodeConnectorType.NONE)
        {
            NodeConnector connector = FindNodeConnectorWithAllowedType(type, connectorType);

            if (connector == null)
                return new List<Node>();

            List<Node> connectedNodes = new List<Node>();
            foreach(NodeConnector connectedConnector in connector.ConnectedNodeConnectors)
                connectedNodes.Add(connectedConnector.ParentNode);

            return connectedNodes;
        }

        private NodeConnector FindNodeConnectorWithAllowedType(NodeType type, NodeConnectorType connectorType = NodeConnectorType.NONE)
        {
            foreach (NodeConnector connector in connectorStore)
            {
                if (connector.AllowedNodeType == type || connector.AllowedNodeType == GetSuperiorType(type) || type == NodeType.NONE)
                {
                    if (connectorType == NodeConnectorType.NONE || connector.Type == connectorType)
                        return connector;
                }
            }

            return null;
        }

        #endregion
    }

    /// <summary>
    /// Attribute which must be added to every not abstract node in order to work.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class NodeAttribute : Attribute
    {
        /// <summary/>
        public NodeAttribute() => AllowedTypes = null;

        /// <summary>
        /// Name shown in the node selection menu.
        /// </summary>
        public string MenuName { get; set; }
        /// <summary>
        /// Type of this node.
        /// </summary>
        public NodeType Type { get; set; }
        /// <summary>
        /// Contains all NodeTypes which can be connected to this node.
        /// Should be the same types as the input or output connector.
        /// <para>Don't use superior node types like NodeType.GENERAL here!</para>
        /// </summary>
        public NodeType[] AllowedTypes { get; set; }
    }
}
