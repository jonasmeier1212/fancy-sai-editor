using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Shapes;

namespace NodeAI
{
    public class NodeManager
    {
        public NodeManager(Canvas _nodeEditor, ScaleTransform _scaleTransform)
        {
            nodeEditor = _nodeEditor;
            nodeEditorScaleTransform = _scaleTransform;

            nodeTrees = new List<NodeTree>();
            selectedNodes = new HashSet<Node>();
            copiedNodes = new List<Node>();
        }

        private static NodeManager _instance = null;

        public static NodeManager Instance
        {
            get { Debug.Assert(_instance != null); return _instance; }
            set { Debug.Assert(_instance == null); _instance = value; }
        }

        /// <summary>
        /// Adds a node to it's corresponding node tree and manages positioning of this node.
        /// If the creator node is given the created node is going to be connected to the creator.
        /// </summary>
        public void AddNode(Node _node, Node _creator = null)
        {
            if (!nodeEditor.Children.Contains(_node))
            {
                //Very important to do this first, because otherwise the connectors crashes because the visual parents aren't existing
                nodeEditor.Children.Add(_node);
                Canvas.SetTop(_node, 0); //Init default canvas position
                Canvas.SetLeft(_node, 0);
                nodeEditor.UpdateLayout();
            }

            //Important: First handle connections and then sort in a node tree to ensure a correct classification!
            if (_creator != null)
                ConnectNodes(_creator, _node);
            
            //If the new node is in no tree created a new for this node
            if(_node.NodeTree == null)
            {
                nodeTrees.Add(new NodeTree(_node, nodeEditor));
            }
        }

        /// <summary>
        /// Tries to connect the two passed nodes
        /// Important: _n1 must have a node tree!
        /// </summary>
        public bool ConnectNodes(Node _n1, Node _n2)
        {
            _n1.NodeTree.ConnectNode(_n2, _n1);
            return false;
        }

        /// <summary>
        /// Copies the selected nodes.
        /// </summary>
        public void CopyNodes()
        {
            copiedNodes = selectedNodes.ToList();
        }

        /// <summary>
        /// Pastes the copied nodes.
        /// Before pasting a node must be copied.
        /// </summary>
        public void PasteNodes()
        {
            foreach(Node copiedNode in copiedNodes)
            {
                Node newNode = copiedNode.Clone();
                AddNode(newNode);
                //TODO: When connected nodes are copied connect the cloned nodes like the copied were connected
            }
        }

        /// <summary>
        /// Deletes the selected nodes
        /// </summary>
        public void DeleteNodes()
        {
            foreach(Node selectedNode in selectedNodes)
            {
                selectedNode.NodeTree.RemoveNode(selectedNode);
                nodeEditor.Children.Remove(selectedNode);
            }
        }

        /// <summary>
        /// Selects the passed nodes
        /// </summary>
        public void SelectNodes(List<Node> _nodes)
        {
            selectedNodes.UnionWith(_nodes);
            foreach (Node selectedNode in selectedNodes)
                selectedNode.Select();
        }

        /// <summary>
        /// Selects all nodes
        /// </summary>
        public void SelectAllNodes()
        {
            foreach(NodeTree tree in nodeTrees)
            {
                foreach(Node node in tree.GetNodes())
                {
                    node.Select();
                    selectedNodes.Add(node);
                }
            }
        }

        /// <summary>
        /// Selects the passed node
        /// </summary>
        public void SelectNode(Node _node)
        {
            selectedNodes.Add(_node);
            _node.Select();
        }

        /// <summary>
        /// Deselects the selected nodes if one is selected
        /// </summary>
        public void DeselectNodes()
        {
            foreach (Node selectedNode in selectedNodes)
                selectedNode.Deselect();
            selectedNodes.Clear();
        }

        public void SelectNodesInRect(Rectangle rect)
        {
            Rect selectionRect = new Rect(Canvas.GetLeft(rect), Canvas.GetTop(rect), rect.ActualWidth, rect.ActualHeight);
            Rect nodeRect = default(Rect);
            List<Node> selectNodes = new List<Node>();
            foreach(NodeTree tree in nodeTrees)
            {
                foreach(Node node in tree.GetNodes())
                {
                    nodeRect.X = Canvas.GetLeft(node);
                    nodeRect.Y = Canvas.GetTop(node);
                    nodeRect.Width = node.ActualWidth;
                    nodeRect.Height = node.ActualHeight;

                    if (selectionRect.IntersectsWith(nodeRect))
                        selectNodes.Add(node);
                }
            }
            SelectNodes(selectNodes);
        }

        /// <summary>
        /// Scales the editor for the given value
        /// </summary>
        public void Scale(float _delta)
        {
            //Take mouse position as scale center point
            nodeEditorScaleTransform.CenterX = Mouse.GetPosition(nodeEditor).X;
            nodeEditorScaleTransform.CenterY = Mouse.GetPosition(nodeEditor).Y;
            if (_delta > 0)
            {
                nodeEditorScaleTransform.ScaleX *= Properties.Settings.Default.ScaleRatio;
                nodeEditorScaleTransform.ScaleY *= Properties.Settings.Default.ScaleRatio;
            }
            else
            {
                nodeEditorScaleTransform.ScaleX /= Properties.Settings.Default.ScaleRatio;
                nodeEditorScaleTransform.ScaleY /= Properties.Settings.Default.ScaleRatio;
            }
            
        }

        /// <summary>
        /// Repositions every node view tree and removes every collision
        /// </summary>
        public void Position()
        {
            foreach (NodeTree tree in nodeTrees)
                tree.AutoPosition();
        }
        
        /// <summary>
        /// Updates everything belonging to nodes
        /// </summary>
        public void Update()
        {
            List<NodeTree> outdatetTrees = new List<NodeTree>();
            foreach(NodeTree nodeTree in nodeTrees)
            {
                if (!nodeTree.Update())
                    outdatetTrees.Add(nodeTree);
            }
            foreach (NodeTree tree in outdatetTrees)
                nodeTrees.Remove(tree);
        }

        /// <summary>
        /// Sets the position of the passed node to passed point
        /// </summary>
        /// <param name="_node"></param>
        public void SetPosition(Node _node, Point _position)
        {
            Canvas.SetLeft(_node, _position.X);
            Canvas.SetTop(_node, _position.Y);
        }

        /// <summary>
        /// Displace the passed node with the passed offset values
        /// </summary>
        public void SetPosition(Node _node, double _offsetX, double _offsetY)
        {
            double x = Canvas.GetLeft(_node) + _offsetX;
            double y = Canvas.GetTop(_node) + _offsetY;

            Canvas.SetLeft(_node, x);
            Canvas.SetTop(_node, y);
        }

        /// <summary>
        /// Exports every complete and valid node tree
        /// </summary>
        public void Export()
        {
            foreach (NodeTree tree in nodeTrees)
                SqlExporter.ExportNodeTree(tree);
        }

        /// <summary>
        /// Exports the selected nodes trees associated with the selected nodes
        /// </summary>
        public void ExportSelectedNodeTrees()
        {
            foreach (NodeTree tree in GetNodeTrees(selectedNodes.ToList()))
                SqlExporter.ExportNodeTree(tree);
        }

        /// <summary>
        /// Returns a set with all node trees the passed nodes contains
        /// </summary>
        private HashSet<NodeTree> GetNodeTrees(List<Node> _nodes)
        {
            HashSet<NodeTree> nodeTrees = new HashSet<NodeTree>();

            foreach (Node node in _nodes)
                nodeTrees.Add(node.NodeTree);

            return nodeTrees;
        }

        public void InitDrag(Point initPosition)
        {
            anchorPoint = initPosition;
            Mouse.OverrideCursor = Cursors.ScrollAll;
        }

        public void ProcessDrag(Point dragPosition)
        {
            if (anchorPoint == default(Point))
                return;

            double offsetX = dragPosition.X - anchorPoint.X;
            double offsetY = dragPosition.Y - anchorPoint.Y;

            foreach(Node node in selectedNodes)
                SetPosition(node, offsetX, offsetY);

            foreach(NodeTree tree in nodeTrees)
            {
                if(tree.GetNodes().All(i=>selectedNodes.Contains(i)))
                {
                    tree.Move(offsetX, offsetY);
                }
            }

            anchorPoint = dragPosition;

        }

        public void EndDrag()
        {
            Mouse.OverrideCursor = Cursors.Arrow;
            anchorPoint = default(Point);
        }

        private Point anchorPoint;

        private Canvas nodeEditor;
        private ScaleTransform nodeEditorScaleTransform;
        private List<NodeTree> nodeTrees;
        private HashSet<Node> selectedNodes;
        private List<Node> copiedNodes;
    }
}
