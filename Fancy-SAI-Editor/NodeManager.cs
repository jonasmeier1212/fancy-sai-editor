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

namespace NodeAI
{
    public class NodeManager
    {
        public NodeManager(Canvas _nodeEditor, ScaleTransform _scaleTransform)
        {
            nodeEditor = _nodeEditor;
            nodeEditorScaleTransform = _scaleTransform;

            nodeTrees = new List<NodeTree>();
            selectedNodes = new List<Node>();
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
        public void AddNode(Node node, Node creator = null)
        {
            //Important: First handle connections and then sort in a node tree to ensure a correct classification!
            if (creator != null)
            {
                if (creator.ConnectToNode(node))
                    node.NodeTree = creator.NodeTree;
            }
            
            //If the new node is in no tree created a new for this node
            if(node.NodeTree == null)
            {
                NodeTree newTree = new NodeTree();
                nodeTrees.Add(newTree);
                node.NodeTree = newTree;
            }

            node.NodeTree.AddNode(node); //Add the node to its node tree to handle positioning etc..
            nodeEditor.Children.Add(node);
        }

        /// <summary>
        /// Copies the selected nodes.
        /// </summary>
        public void CopyNodes()
        {
            copiedNodes = selectedNodes;
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
            selectedNodes = _nodes;
            foreach (Node selectedNode in selectedNodes)
                selectedNode.Select();
        }

        /// <summary>
        /// Selects the passed node
        /// </summary>
        public void SelectNode(Node _node)
        {
            selectedNodes.Clear();
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

        /// <summary>
        /// Scales the editor for the given value
        /// </summary>
        public void Scale(float delta)
        {
            //Take mouse position as scale center point
            nodeEditorScaleTransform.CenterX = Mouse.GetPosition(nodeEditor).X;
            nodeEditorScaleTransform.CenterY = Mouse.GetPosition(nodeEditor).Y;
            if (delta > 0)
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
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Updates everything belonging to nodes
        /// </summary>
        public void Update()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Initiates the drag of a node from the given start point.
        /// This start point is used to determine how far and in which direction the drag is going to be performed
        /// </summary>
        public void InitDrag(Point _initPoint)
        {
            
        }

        /// <summary>
        /// Connects two node connectors visually with each other.
        /// A spline is going to be created and the new connected nodes are going to be sorted in the correct node tree.
        /// </summary>
        public void ConnectNodeConnectors(NodeConnector origin, NodeConnector target)
        {
            origin.ConnectTo(target);
            target.ConnectTo(origin);
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
            foreach (NodeTree tree in GetNodeTrees(selectedNodes))
                SqlExporter.ExportNodeTree(tree);
        }

        /// <summary>
        /// Returns a list with all node trees the passed nodes contains
        /// </summary>
        private HashSet<NodeTree> GetNodeTrees(List<Node> _nodes)
        {
            HashSet<NodeTree> nodeTrees = new HashSet<NodeTree>();

            foreach (Node node in _nodes)
                nodeTrees.Add(node.NodeTree);

            return nodeTrees;
        }

        private Canvas nodeEditor;
        private ScaleTransform nodeEditorScaleTransform;
        private List<NodeTree> nodeTrees;
        private List<Node> selectedNodes;
        private List<Node> copiedNodes;
    }
}
