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

            nodes = new HashSet<Node>();
            visualConnectionsStore = new List<VisualConnection>();
            visualBuckets = new SortedDictionary<int, VisualBucket>();
            nodes.Add(_creatorNode);
            _creatorNode.NodeTree = this;

            visualBuckets[0] = new VisualBucket();

            visualBuckets[0].Add(_creatorNode);

            position = new Point()
            {
                X = Canvas.GetLeft(_creatorNode),
                Y = Canvas.GetTop(_creatorNode),
            };
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
        /// Connects a node to a node inside this tree.
        /// The old tree of the node is combined with this tree.
        /// </summary>
        public void ConnectNode(Node _node, Node _originNode)
        {
            // Node tree of _connectTo node must be this tree!
            Debug.Assert(_originNode.NodeTree == this);

            if (_node.GetDirectlyConnectedNodes(NodeType.NONE).Contains(_originNode))
                return;

            NodeConnector origin = null;
            NodeConnector target = null;
            foreach(NodeConnector originConnector in _originNode.Connectors)
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
            {
                MessageBox.Show("Nodes can't be connected!");
                return;
            }

            //Link the connectors
            origin.ConnectTo(target);
            target.ConnectTo(origin);

            //Create visual connection
            visualConnectionsStore.Add(new VisualConnection(origin, target, nodeEditor));

            //Combine the two trees if the new node has one
            if (_node.NodeTree != null && _node.NodeTree != this)
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

            //Sort the new node in right bucket
            int index = GetNodeVisualBucketIndex(_originNode);

            if (origin.Type == NodeConnectorType.INPUT)
                index--;
            else if (origin.Type == NodeConnectorType.OUTPUT)
                index++;

            if (!visualBuckets.ContainsKey(index))
                visualBuckets[index] = new VisualBucket();

            visualBuckets[index].Add(_node);
            AutoPosition();
        }

        /// <summary>
        /// Removes the passed node from this tree
        /// </summary>
        public void RemoveNode(Node _node)
        {
            if (!nodes.Remove(_node))
                return;
            
            //Remove node from visual bucket
            foreach(VisualBucket bucket in visualBuckets.Values)
            {
                if (bucket.HasNode(_node))
                    bucket.Remove(_node);
            }

            //Remove attached visual connections
            var connectionsToRemove = visualConnectionsStore.Where(connection =>
            {
                foreach(var connector in _node.Connectors)
                {
                    if (connection.ContainConnector(connector))
                        return true;
                }

                return false;
            });
            foreach(var c in connectionsToRemove)
                c.Remove();
            visualConnectionsStore.RemoveAll(c => connectionsToRemove.Contains(c));

            RecalcSize();
            nodeEditor.Children.Remove(_node);
        }

        /// <summary>
        /// Returns the number of nodes in this tree
        /// </summary>
        public int NodeCount()
        {
            return nodes.Count;
        }

        public void AutoPosition()
        {
            double offsetX = position.X;
            double offsetY = 0;
            //Find bucket with highest height
            double maxHeight = 0;
            foreach (VisualBucket bucket in visualBuckets.Values)
            {
                if (bucket.Height > maxHeight)
                    maxHeight = bucket.Height;
            }

            foreach (VisualBucket bucket in visualBuckets.Values)
            {
                offsetY = (maxHeight - bucket.Height) / 2 + position.Y;
                bucket.Update(new Point(offsetX, offsetY));
                offsetX += bucket.Width + maxHeight / 10;
            }
        }

        private int GetNodeVisualBucketIndex(Node _node)
        {
            foreach(var bucketKeyPair in visualBuckets)
            {
                if (bucketKeyPair.Value.HasNode(_node))
                    return bucketKeyPair.Key;
            }

            Debug.Assert(true, "Node isn't in any bucket! Something went wrong at creation!");
            return int.MinValue;
        }

        public List<Nodes.GeneralNodes.Npc> GetSAIOwnerNodes()
        {
            List<Nodes.GeneralNodes.Npc> saiOwnerNodes = new List<Nodes.GeneralNodes.Npc>();
            foreach(Node node in nodes)
            {
                if(node is Nodes.GeneralNodes.Npc npcNode)
                {
                    if (npcNode.IsSAIOwner())
                        saiOwnerNodes.Add(npcNode);
                }
            }
            return saiOwnerNodes;
        }

        public List<Node> GetNodes()
        {
            return nodes.ToList();
        }

        public void Move(double offsetX, double offsetY)
        {
            position.Offset(offsetX, offsetY);
        }

        public void RecalcSize()
        {
            foreach (VisualBucket bucket in visualBuckets.Values)
                bucket.RecalcSize();
            AutoPosition();
        }

        private HashSet<Node> nodes; //TODO: Is it needed to store the nodes here? Because they are already in their buckets!
        private List<VisualConnection> visualConnectionsStore;
        private Canvas nodeEditor;
        private SortedDictionary<int, VisualBucket> visualBuckets;
        private Point position;

        private class VisualBucket
        {
            public VisualBucket()
            {
                nodes = new HashSet<Node>();
                width = 0;
                height = 0;
                position = new Point(0, 0);
            }

            public void Add(Node node)
            {
                if(node.ActualWidth > width)
                    width = node.ActualWidth;

                height += node.ActualHeight;

                node.VerticalPositionIndex = nodes.Count;

                nodes.Add(node);
            }

            public void Remove(Node node)
            {
                nodes.Remove(node);
            }

            public void Update(Point _position = default(Point))
            {
                var positionNodes = nodes.ToList();
                positionNodes.Sort(new ByVerticalPosition());

                if (_position != default(Point))
                    position = _position;

                double offset = position.Y;
                foreach (Node node in positionNodes)
                {
                    Canvas.SetTop(node, offset);
                    offset += node.ActualHeight + 10;
                    Canvas.SetLeft(node, position.X + (Width - node.ActualWidth) / 2);
                }
            }

            public void RecalcSize()
            {
                height = 0;
                foreach(Node node in nodes)
                {
                    if (node.ActualWidth > width)
                        width = node.ActualWidth;

                    height += node.ActualHeight;
                }
            }

            public bool HasNode(Node _node)
            {
                return nodes.Contains(_node);
            }

            HashSet<Node> nodes;
            double width;
            double height;
            Point position;

            public double Width { get => width; }
            public double Height { get => height; }

            private class ByVerticalPosition : IComparer<Node>
            {
                public int Compare(Node n1, Node n2)
                {
                    return CalcPositionIndex(n1) - CalcPositionIndex(n2);
                }

                private int CalcPositionIndex(Node _node)
                {
                    int index = 0;
                    var inputs = _node.GetDirectlyConnectedNodes(NodeType.NONE, NodeConnectorType.INPUT);
                    if (inputs.Count > 0 && inputs.First() is Node input)
                    {
                        index += input.VerticalPositionIndex * 10; //Weight the input node position index more than the connector position index
                        if (input.GetConnectorConnectedTo(_node) is NodeConnector connector1)
                            index += connector1.PositionIndex;
                        if (_node.GetConnectorConnectedTo(input) is NodeConnector connector2)
                            index += connector2.PositionIndex;
                    }
                    var outputs = _node.GetDirectlyConnectedNodes(NodeType.NONE, NodeConnectorType.OUTPUT);
                    if (outputs.Count > 0 && outputs.First() is Node output) //TODO: There can be more than one output
                    {
                        index += output.VerticalPositionIndex * 10; //Weight the input node position index more than the connector position index
                        if (output.GetConnectorConnectedTo(_node) is NodeConnector connector1)
                            index += connector1.PositionIndex;
                        if (_node.GetConnectorConnectedTo(output) is NodeConnector connector2)
                            index += connector2.PositionIndex;
                    }

                    return index;
                }
            }
        }

        private class VisualConnection
        {
            public VisualConnection(NodeConnector _connector1, NodeConnector _connector2, Canvas _nodeEditor)
            {
                connector1 = _connector1;
                connector2 = _connector2;
                nodeEditor = _nodeEditor;

                Ellipse e1 = connector1.ellipse;
                Ellipse e2 = connector2.ellipse;

                connectionPath = new Path
                {
                    Stroke = new SolidColorBrush(Properties.Settings.Default.ConnectionPathColor),
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

            public void Remove()
            {
                nodeEditor.Children.Remove(connectionPath);
            }

            public bool ContainConnector(NodeConnector _connector)
            {
                if (connector1 == _connector || connector2 == _connector)
                    return true;
                return false;
            }

            private NodeConnector connector1;
            private NodeConnector connector2;
            private Canvas nodeEditor;
            private PathFigure connection;
            private Path connectionPath;
        }
    }
}
