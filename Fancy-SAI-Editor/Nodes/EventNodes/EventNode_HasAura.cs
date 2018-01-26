using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_RESPAWN
    /// 
    /// </summary>
    [Node(MenuName = "Has aura", Type = NodeType.EVENT_HAS_AURA, AllowedTypes = new NodeType[] { NodeType.GENERAL_SPELL, NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class HasAura : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public HasAura()
        {
            Type = NodeType.EVENT_HAS_AURA;

            EventId = "23";

            //Update text
            NodeName.Content = "Has aura";

            AddParam<GeneralNodes.Spell>(ParamId.PARAM_1, NodeType.GENERAL_SPELL, "Spell");
            AddParam(ParamId.PARAM_2, "Stacks:");
            AddParam(ParamId.PARAM_3, "Repeat min:");
            AddParam(ParamId.PARAM_4, "Repeat max:");
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new HasAura();
        }
    }
}
