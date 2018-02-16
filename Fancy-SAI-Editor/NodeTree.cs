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
    public class NodeTree
    {
        public NodeTree(Node _creatorNode, Canvas _nodeEditor)
        {
            nodeEditor = _nodeEditor;

            nodes = new List<Node>();
            visualConnectionsStore = new List<VisualConnection>();
            height = 0;
            width = 0;
            position = new Point();
            nodes.Add(_creatorNode);
            _creatorNode.NodeTree = this;
        }

        /// <summary>
        /// Updates all visual elements.
        /// Returns false if this tree contains no nodes and can be deleted.
        /// </summary>
        public bool Update()
        {
            if (nodes.Count == 0)
                return false;

            foreach (VisualConnection visual in visualConnectionsStore)
                visual.Update();

            return true;
        }

        /// <summary>
        /// Adds a node to this tree adding connection and handling positioning.
        /// The old tree of the node is combined with this tree.
        /// </summary>
        public void AddNode(Node _node, Node _connectTo)
        {
            // Node tree of _connectTo node must be this tree!
            Debug.Assert(_connectTo.NodeTree == this);

            NodeConnector origin = null;
            NodeConnector target = null;
            foreach(NodeConnector originConnector in _connectTo.Connectors)
            {
                foreach(NodeConnector targetConnector in _node.Connectors)
                {
                    if(originConnector.CanConnect(targetConnector) && targetConnector.CanConnect(originConnector))
                    {
                        origin = originConnector;
                        target = targetConnector;
                    }
                }
            }

            //Check if no connection possible
            if (origin == null || target == null)
                return;

            //Link the connectors
            origin.ConnectTo(target);
            target.ConnectTo(origin);

            //Create visual connection
            visualConnectionsStore.Add(new VisualConnection(origin, target, nodeEditor));

            //Combine the two trees if the new node has one
            if (_node.NodeTree != null)
            {
                if (_node.NodeTree.NodeCount() > 0)
                {
                    List<Node> removeNodes = new List<Node>();
                    foreach (Node node in _node.NodeTree.nodes)
                    {
                        //TODO: Tranfer connections from the old tree
                        nodes.Add(node);
                        removeNodes.Add(node);
                    }
                    foreach (Node node in removeNodes)
                        _node.NodeTree.RemoveNode(node);
                }
            }
            else
                _node.NodeTree = this;

            nodes.Add(_node);

            //Recalc height and width
            //TODO!
        }

        /// <summary>
        /// Removes the passed node from this tree
        /// </summary>
        public void RemoveNode(Node _node)
        {
            nodes.Remove(_node);
        }

        /// <summary>
        /// Returns the number of nodes in this tree
        /// </summary>
        public int NodeCount()
        {
            return nodes.Count;
        }

        private List<Node> nodes; //I don't think this is really needed but eventually it's handy to have
        private List<VisualConnection> visualConnectionsStore;
        private Canvas nodeEditor;
        private Point position;
        private int height;
        private int width;

        private class VisualConnection
        {
            public VisualConnection(NodeConnector _connector1, NodeConnector _connector2, Canvas _nodeEditor)
            {
                connector1 = _connector1;
                connector2 = _connector2;
                nodeEditor = _nodeEditor;

                Ellipse e1 = connector1.ellipse;
                Ellipse e2 = connector2.ellipse;

                Path connectionPath = new Path
                {
                    Stroke = Brushes.LightBlue,
                };

                Point p1 = new Point(e1.TransformToAncestor(nodeEditor).Transform(new Point(0, 0)).X + e1.ActualWidth / 2, (e1.TransformToAncestor(nodeEditor).Transform(new Point(0, 0)).Y + e1.ActualHeight / 2));
                Point p2 = new Point(e2.TransformToAncestor(nodeEditor).Transform(new Point(0, 0)).X + e2.ActualWidth / 2, (e2.TransformToAncestor(nodeEditor).Transform(new Point(0, 0)).Y + e2.ActualHeight / 2));

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

                connectionPath.Data = connectionGeometry;

                connection = pathFigure;
                nodeEditor.Children.Add(connectionPath);
            }

            public void Update()
            {
                Point p1 = new Point(connector1.ellipse.TransformToAncestor(nodeEditor).Transform(new Point(0, 0)).X + connector1.ellipse.ActualWidth / 2, (connector1.ellipse.TransformToAncestor(nodeEditor).Transform(new Point(0, 0)).Y + connector1.ellipse.ActualHeight / 2));
                Point p2 = new Point(connector2.ellipse.TransformToAncestor(nodeEditor).Transform(new Point(0, 0)).X + connector2.ellipse.ActualWidth / 2, (connector2.ellipse.TransformToAncestor(nodeEditor).Transform(new Point(0, 0)).Y + connector2.ellipse.ActualHeight / 2));
                connection.StartPoint = p1;

                (connection.Segments.First() as BezierSegment).Point1 = new Point(p1.X + (p2.X - p1.X) * 0.66, p1.Y);
                (connection.Segments.First() as BezierSegment).Point2 = new Point(p2.X + (p1.X - p2.X) * 0.66, p2.Y);
                (connection.Segments.First() as BezierSegment).Point3 = p2;
            }

            private NodeConnector connector1;
            private NodeConnector connector2;
            private Canvas nodeEditor;
            private PathFigure connection;
        }
    }
}
