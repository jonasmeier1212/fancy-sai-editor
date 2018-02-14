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
        /// Adds a node to this tree and position it in this tree
        /// </summary>
        public void AddNode(Node _node)
        {
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

        private List<Node> nodes; //I don't think this is really needed but eventually it's handy to have
        private Point position;
        private int height;
        private int width;
    }
}
