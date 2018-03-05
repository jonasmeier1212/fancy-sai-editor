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
using System.Diagnostics;

namespace FancySaiEditor
{
    /// <summary>
    /// Interaktionslogik für NodeConnector.xaml
    /// </summary>
    public partial class NodeConnector : UserControl
    {
        /// <summary>
        /// Initializes a new instance of a NodeConnector with the passed parameters.
        /// </summary>
        /// <param name="_label">In the node shown name of the connector.</param>
        /// <param name="_type">Determines if it's an input or an output connector</param>
        /// <param name="_parentNode">Node which contains this new NodeConnector.</param>
        /// <param name="_allowedNodeType">Type of node which is allowed to connector to this connector. If you want all nodes of a base type to be allowed use EVENT, ACTION or GENERAL</param>
        /// <param name="_allowedConnectionCount">Number of allowed connections to this node</param>
        public NodeConnector(string _label, NodeConnectorType _type, Node _parentNode, NodeType _allowedNodeType, int _positionIndex, int _allowedConnectionCount = 1)
        {
            InitializeComponent();

            Debug.Assert(_type != NodeConnectorType.NONE); //A node connector can't have NONE as type. It must be either an input or an output connector
            type = _type;
            ParentNode = _parentNode;
            allowedNodeType = _allowedNodeType;
            allowedConnectionCount = _allowedConnectionCount;
            positionIndex = _positionIndex;

            ellipse.Fill = type == NodeConnectorType.INPUT ? Brushes.Aqua : Brushes.IndianRed;
            ellipse.AllowDrop = true;

            if(Node.GetSuperiorType(allowedNodeType) != NodeType.NONE)
            {
                ToolTip toolTip = new ToolTip()
                {
                    Content = "Right click for creation of corresponding node."
                };
                ellipse.ToolTip = toolTip;
            }

            label.Content = _label;

            if (type == NodeConnectorType.PARAM)
            {
                HorizontalAlignment = HorizontalAlignment.Center;
                label.HorizontalAlignment = HorizontalAlignment.Center;
                Grid.SetRow(ellipse, 1);
            }
            else
            {
                //If it's an input connector the ellipse must be first element respectively the element on the left
                ellipse.SetValue(Grid.ColumnProperty, type == NodeConnectorType.INPUT ? 0 : 1);
                label.SetValue(Grid.ColumnProperty, type == NodeConnectorType.INPUT ? 1 : 0);
            }

            connectedNodeConnectors = new List<NodeConnector>();
        }

        /// <summary>
        /// Checks if the passed node connector can connect to this connector.
        /// </summary>
        public bool CanConnect(NodeConnector target)
        {
            if (ConnectedNodeConnectors.Count <= allowedConnectionCount)
            {
                if (target.ParentNodeType == allowedNodeType || Node.GetSuperiorType(target.ParentNodeType) == allowedNodeType || AllowedNodeType == NodeType.NONE)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if the passed node type can connect to this connector.
        /// </summary>
        public bool CanConnect(NodeType targetType)
        {
            if (ConnectedNodeConnectors.Count < allowedConnectionCount)
            {
                if (targetType == allowedNodeType || Node.GetSuperiorType(targetType) == allowedNodeType || AllowedNodeType == NodeType.NONE)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Node which contains this connector.
        /// </summary>
        public Node ParentNode { get; }
        /// <summary>
        /// Parent node type of this nodeConnector.
        /// </summary>
        public NodeType ParentNodeType { get => ParentNode.Type; }
        /// <summary>
        /// Allowed node type for this connector.
        /// </summary>
        public NodeType AllowedNodeType { get => allowedNodeType; }
        /// <summary>
        /// Type of this connector
        /// </summary>
        public NodeConnectorType Type { get => type; }
        /// <summary>
        /// Allowed number of connections.
        /// </summary>
        public int AllowedConnectionCount { get => allowedConnectionCount; }
        /// <summary>
        /// List with all connected node connector.
        /// </summary>
        public List<NodeConnector> ConnectedNodeConnectors { get => connectedNodeConnectors; }
        /// <summary>
        /// Position index of this connector.
        /// Indicates the vertical position of the connector.
        /// Must be unique for input or output connectors in one node!
        /// </summary>
        public int PositionIndex { get => positionIndex; }

        private NodeConnectorType type;
        private NodeType allowedNodeType;
        private List<NodeConnector> connectedNodeConnectors;
        private int allowedConnectionCount;
        private int positionIndex;

        /// <summary>
        /// Connects this node connector to target node connector
        /// </summary>
        public bool ConnectTo(NodeConnector target)
        {
            if (ConnectedNodeConnectors.Count < allowedConnectionCount)
            {
                ConnectedNodeConnectors.Add(target);
                return true;
            }
            return false;
        }

        private void HandleDragInitiation(object sender, MouseEventArgs e)
        {
            if (sender is Ellipse && e.LeftButton == MouseButtonState.Pressed)
            {
                if (ParentNode.MainWindow.IsSelecting())
                    return;

                DataObject data = new DataObject("originConnector", this);
                DragDrop.DoDragDrop(ellipse, data, DragDropEffects.Copy);
            }
        }

        private void HandleMouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Ellipse ellipse)
            {
                ellipse.Stroke = Brushes.White;
            }
        }

        private void HandleMouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Ellipse ellipse)
            {
                ellipse.Stroke = null;
            }
        }

        private void HandleDrop(object sender, DragEventArgs e)
        {
            Node origin = Utility.GetParentWithType<Node>(e.Data.GetData("originConnector"));
            if (origin != null)
                NodeManager.Instance.ConnectNodes(ParentNode, origin);
        }

        private void HandleDragOver(object sender, DragEventArgs e)
        {
            //Check if the drop is valid
            e.Effects = DragDropEffects.None;
            try
            {
                NodeConnector c1 = Utility.GetParentWithType<NodeConnector>(sender);
                NodeConnector c2 = e.Data.GetData("originConnector") as NodeConnector;

                if (c1.CanConnect(c2) && c2.CanConnect(c1))
                    e.Effects = DragDropEffects.Copy;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Invalid node connector objects", "Error", MessageBoxButton.OKCancel);
            }
        }

        private void HandleRightClick(object sender, MouseButtonEventArgs e)
        {
            if (ConnectedNodeConnectors.Count < AllowedConnectionCount)
            {
                //Create the appropriate node if allowed node is a specific node
                if (AllowedNodeType != NodeType.NONE && AllowedNodeType != NodeType.ACTION && AllowedNodeType != NodeType.EVENT && AllowedNodeType != NodeType.GENERAL && AllowedNodeType != NodeType.TARGET)
                {
                    if (ParentNode.MainWindow != null)
                    {
                        Node newNode = NodeManager.Instance.CreateNode(AllowedNodeType, ParentNode);
                        ParentNode.MainWindow.UpdateLayout();
                    }
                }
                else
                {
                    ParentNode.MainWindow.ShowNodeSelectionMenuInNodeEditor(this);
                }
                e.Handled = true;
            }
        }
    }
}
