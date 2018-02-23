using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancySaiEditor.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_JUST_SUMMONED
    ///
    /// </summary>
    [Node(MenuName = "Just summoned", Type = NodeType.EVENT_JUST_SUMMONED, AllowedTypes = new NodeType[] { NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class JustSummoned : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public JustSummoned()
        {
            Type = NodeType.EVENT_JUST_SUMMONED;

            

            //Update text
            NodeName.Content = "Just summoned";
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new JustSummoned();
        }
    }
}
