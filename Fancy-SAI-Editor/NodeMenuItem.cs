using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace FancySaiEditor
{
    /// <summary>
    /// Class for all menu items whose purpose is the creation of nodes
    /// </summary>
    public class NodeMenuItem : MenuItem
    {
        /// <summary>
        /// Creates an instance without a origin node.
        /// </summary>
        /// <param name="type">Type of the node to be created.</param>
        public NodeMenuItem(NodeType type)
        {
            Type = type;
            Origin = null;
            CreatePosition = new Point(0,0);
        }

        /// <summary>
        /// Creates an instance with origin node.
        /// After creation this node is going to be connected to the origin node.
        /// </summary>
        /// <param name="type">Type of the node to be created.</param>
        /// <param name="origin">Origin node to be connected with.</param>
        public NodeMenuItem(NodeType type, Node origin)
        {
            Type = type;
            Origin = origin;
            CreatePosition = new Point(0, 0);
        }

        /// <summary>
        /// Creates an instance without an origin node.
        /// This node is going to be created at the passed position
        /// </summary>
        public NodeMenuItem(NodeType type, Point createPosition)
        {
            Type = type;
            CreatePosition = createPosition;
        }

        /// <summary>
        /// Type of the node to be created on click on this menu item
        /// </summary>
        public NodeType Type { get; private set; }
        /// <summary>
        /// Origin node to be connected with after node creation.
        /// </summary>
        public Node Origin { get; private set; }

        /// <summary>
        /// Position where the node is going to be created
        /// </summary>
        public Point CreatePosition { get; private set; }
    }
}
