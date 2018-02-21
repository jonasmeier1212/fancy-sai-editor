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
using System.Diagnostics;

namespace NodeAI
{
    public delegate string NodeParamGetCallback();
    public delegate void NodeParamSetCallback(string value);

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
            Deselect(); //Node shouldn't be selected by default
            LoadTooltip();
            connectorStore = new List<NodeConnector>();
            paramStore = new Dictionary<ParamId, NodeParam>();
            nodeTree = null;
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
        private NodeTree nodeTree;
        private NodeType type;
        private List<NodeConnector> connectorStore;
        private NodeData nodeData;
        private Dictionary<ParamId, NodeParam> paramStore; //The delegate returns the method the value of this parameter

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
        /// Stores in which node tree this node is
        /// </summary>
        public NodeTree NodeTree { get => nodeTree; set => nodeTree = value; }

        /// <summary>
        /// Stores the data of the Node. Mostly loaded from the database. Prefer usage of an instance of a derivation class from DataTable in DataStructures.cs
        /// </summary>
        public NodeData NodeData { get => nodeData; set => nodeData = value; }

        /// <summary>
        /// Stores all connectors of this node.
        /// </summary>
        public List<NodeConnector> Connectors { get => connectorStore; }

        #endregion

        /// <summary>
        /// Checks if this node is connected with the passed node
        /// </summary>
        public bool IsConnectedWith(Node _node)
        {
            foreach(NodeConnector connector in connectorStore)
            {
                foreach(NodeConnector connectedConnector in connector.ConnectedNodeConnectors)
                {
                    if (connectedConnector.ParentNode == _node)
                        return true;
                }
            }

            return false;
        }

        public NodeConnector GetConnectorConnectedTo(Node node)
        {
            foreach(NodeConnector connector in connectorStore)
            {
                foreach(NodeConnector connected in connector.ConnectedNodeConnectors)
                {
                    if (connected.ParentNode == node)
                        return connector;
                }
            }

            return null;
        }

        public int VerticalPositionIndex { get; set; }
        public int HorizontalPositionIndex { get; set; }

        #region Node Layout

        #region Params

        class NodeParam
        {
            public NodeParam(NodeParamGetCallback _getCallback, NodeParamSetCallback _setCallback)
            {
                getCallback = _getCallback;
                setCallback = _setCallback;
            }

            public string Get()
            {
                return getCallback();
            }

            public void Set(string value)
            {
                setCallback(value);
            }

            private NodeParamGetCallback getCallback;
            private NodeParamSetCallback setCallback;
        }
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

            paramStore.Add(id, new NodeParam(
                () => 
                { return input.Text; },

                (value) => 
                { input.Text = value.ToString();
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

            paramStore.Add(id, new NodeParam(
                () =>
                {
                    return (Enum.Parse(typeof(T), (selection.SelectedValue as ComboBoxItem).Content.ToString())).ToString();
                },

                (value) =>
                {
                    try
                    {
                        selection.SelectedValue = Enum.GetName(typeof(T), Convert.ToInt32(value));
                    }
                    catch(Exception)
                    {
                        selection.SelectedValue = "Invalid value";
                    }
                }
            ));
        }

        /// <summary>
        /// Adds param as connected node
        /// </summary>
        public void AddParam<T>(ParamId id, NodeType type, string description) where T : Nodes.GeneralNodes.GeneralNode
        {
            AddInputConnector(description, type);
            paramStore.Add(id, new NodeParam(
                () =>
                {
                    Node connectedNode = GetDirectlyConnectedNode(type);
                    if (connectedNode != null && connectedNode is Nodes.GeneralNodes.GeneralNode generalNode)
                        return generalNode.GetParamValue();
                    return "0";
                },

                (value) =>
                {
                    Node connectedNode = MainWindow.CreateNode(type, this);
                    if (connectedNode != null && connectedNode is Nodes.GeneralNodes.GeneralNode generalNode)
                        generalNode.SetParamValue(value);
                }
            ));
        }

        public string GetParam(ParamId id)
        {
            if (paramStore.ContainsKey(id))
                return paramStore[id].Get();

            return "0";
        }

        public void SetParam(ParamId id, string value)
        {
            if (paramStore.ContainsKey(id))
                paramStore[id].Set(value);
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
            int index = 0;
            if (type == NodeConnectorType.INPUT)
                index = (from NodeConnector in connectorStore where NodeConnector.Type == NodeConnectorType.INPUT select NodeConnector).Count();
            else if (type == NodeConnectorType.OUTPUT)
                index = (from NodeConnector in connectorStore where NodeConnector.Type == NodeConnectorType.OUTPUT select NodeConnector).Count();

            NodeConnector newConnector = new NodeConnector(label, type, this, allowedNode, index, nmbAllowedConnections);

            if (type == NodeConnectorType.INPUT)
                inputNodesPanel.Children.Add(newConnector);
            else
                outputNodesPanel.Children.Add(newConnector);
            connectorStore.Add(newConnector);
        }
        #endregion

        #endregion

        #region Drag and Selection Handling

        /// <summary>
        /// Activates visual selection elements
        /// </summary>
        public void Select()
        {
            NodeSelectionBorder.BorderBrush = Brushes.LightSkyBlue;
        }

        /// <summary>
        /// Disables visual selection elements
        /// </summary>
        public void Deselect()
        {
            NodeSelectionBorder.BorderBrush = Brushes.White;
        }

        private void OnLeftMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            if (e.Source is NodeConnector && e.OriginalSource is Ellipse)
                return;
            NodeManager.Instance.InitDrag(Mouse.GetPosition(null));
            NodeManager.Instance.SelectNode(this);
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            //Prevent movement if the mouse is not pressed
            if (Mouse.LeftButton == MouseButtonState.Pressed && !(e.Source is TextBox || e.Source is ComboBox || e.Source is ComboBoxItem)) //TODO: Any better solution for this???
            {
                NodeManager.Instance.ProcessDrag(Mouse.GetPosition(null));
            }
        }

        private void OnLeftMouseUp(object sender, MouseButtonEventArgs e)
        {
            NodeManager.Instance.EndDrag();
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
            List<NodeConnector> connectors = FindNodeConnectorsWithAllowedType(type);

            if (connectors.Count > 1)
            {
                MessageBox.Show("Connector can have more than one node connected!");
                return null;
            }

            foreach (NodeConnector connector in connectors)
            {
                if (connector.AllowedConnectionCount > 1)
                {
                    MessageBox.Show("Connector can have more than one node connected!");
                    return null;
                }

                if (connector.ConnectedNodeConnectors.Count > 0)
                    return connector.ConnectedNodeConnectors.First().ParentNode;
            }
            return null;
        }

        public List<Node> GetDirectlyConnectedNodes(NodeType type, NodeConnectorType connectorType = NodeConnectorType.NONE)
        {
            List<NodeConnector> connectors = FindNodeConnectorsWithAllowedType(type, connectorType);

            List<Node> connectedNodes = new List<Node>();
            foreach (NodeConnector connector in connectors)
            {
                foreach (NodeConnector connectedConnector in connector.ConnectedNodeConnectors)
                    connectedNodes.Add(connectedConnector.ParentNode);
            }

            return connectedNodes;
        }

        private List<NodeConnector> FindNodeConnectorsWithAllowedType(NodeType type, NodeConnectorType connectorType = NodeConnectorType.NONE)
        {
            List<NodeConnector> connectors = new List<NodeConnector>();
            foreach (NodeConnector connector in connectorStore)
            {
                if (connector.AllowedNodeType == type || connector.AllowedNodeType == GetSuperiorType(type) || type == NodeType.NONE)
                {
                    if (connectorType == NodeConnectorType.NONE || connector.Type == connectorType)
                        connectors.Add(connector);
                }
            }

            return connectors;
        }

        public static int GetRealId(NodeType type, NodeType targetType)
        {
            Debug.Assert(targetType == NodeType.EVENT || targetType == NodeType.ACTION || targetType == NodeType.TARGET);

            switch (targetType)
            {
                case NodeType.EVENT:
                    return type - NodeType.EVENT - 1;
                case NodeType.ACTION:
                    return type - NodeType.ACTION - 1;
                case NodeType.TARGET:
                    return type - NodeType.TARGET - 1;
            }
            return 0;
        }

        #endregion

        private void NodeUserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(NodeTree != null)
                NodeTree.RecalcSize();
        }
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
