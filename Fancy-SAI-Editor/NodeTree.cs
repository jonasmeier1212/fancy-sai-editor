using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NodeAI
{
    public class NodeTree
    {
        public NodeTree()
        {
            nodes = new List<Node>();
            height = 0;
            width = 0;
            position = new Point();
        }

        /// <summary>
        /// Adds a node to this tree and position it in this tree.
        /// Also removes the node from its old tree if it had one.
        /// If the old tree had more than one node the two trees are combined.
        /// </summary>
        public void AddNode(Node _node)
        {
            nodes.Add(_node);

            if (_node.NodeTree != null)
            {
                if (_node.NodeTree.nodes.Count == 1)
                    _node.NodeTree.RemoveNode(_node);
                else
                {
                    //Combine the two trees
                    foreach(Node node in _node.NodeTree.nodes)
                    {
                        nodes.Add(node);
                        _node.NodeTree.RemoveNode(node);
                    }
                }
            }

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
        private Point position;
        private int height;
        private int width;
    }
}
