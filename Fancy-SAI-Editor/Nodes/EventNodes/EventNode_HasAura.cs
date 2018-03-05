using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancySaiEditor.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_RESPAWN
    /// 
    /// </summary>
    [Node(MenuName = "Has aura", Type = NodeType.EVENT_HAS_AURA, AllowedTypes = new NodeType[] { NodeType.PARAM_SPELL, NodeType.PARAM_NPC, NodeType.ACTION })]
    public class HasAura : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public HasAura()
        {
            Type = NodeType.EVENT_HAS_AURA;

            

            //Update text
            NodeName.Content = "Has aura";

            AddParam<ParamNodes.Spell>(ParamId.PARAM_1, NodeType.PARAM_SPELL, "Spell");
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
