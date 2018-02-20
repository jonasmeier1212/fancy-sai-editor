using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_ON_SPELLCLICK
    ///
    /// </summary>
    [Node(MenuName = "On spell click", Type = NodeType.EVENT_ON_SPELLCLICK, AllowedTypes = new NodeType[] { NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class OnSpellClick : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public OnSpellClick()
        {
            Type = NodeType.EVENT_ON_SPELLCLICK;

            

            //Update text
            NodeName.Content = "On spell click";
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new OnSpellClick();
        }
    }
}
