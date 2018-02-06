using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_JUST_CREATED
    ///
    /// </summary>
    [Node(MenuName = "Just created", Type = NodeType.EVENT_JUST_CREATED, AllowedTypes = new NodeType[] { NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class JustCreated : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public JustCreated()
        {
            Type = NodeType.EVENT_JUST_CREATED;

            EventId = "63";

            //Update text
            NodeName.Content = "Just created";
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new JustCreated();
        }
    }
}
