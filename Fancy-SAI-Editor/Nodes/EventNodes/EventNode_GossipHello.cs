using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancySaiEditor.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_GOSSIP_HELLO
    ///
    /// </summary>
    [Node(MenuName = "Gossip hello", Type = NodeType.EVENT_GOSSIP_HELLO, AllowedTypes = new NodeType[] { NodeType.AI_OWNER, NodeType.ACTION })]
    public class GossipHello : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public GossipHello()
        {
            Type = NodeType.EVENT_GOSSIP_HELLO;

            

            //Update text
            NodeName.Content = "Gossip hello";
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new GossipHello();
        }
    }
}
