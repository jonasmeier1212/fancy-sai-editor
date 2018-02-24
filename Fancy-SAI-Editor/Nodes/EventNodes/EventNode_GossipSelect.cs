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
    [Node(MenuName = "Gossip select", Type = NodeType.EVENT_GOSSIP_SELECT, AllowedTypes = new NodeType[] { NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class GossipSelect : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public GossipSelect()
        {
            Type = NodeType.EVENT_GOSSIP_SELECT;

            //Update text
            NodeName.Content = "Gossip select";

            AddParam(ParamId.PARAM_1, "Menu ID:");
            AddParam(ParamId.PARAM_2, "ID");
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new GossipSelect();
        }
    }
}
