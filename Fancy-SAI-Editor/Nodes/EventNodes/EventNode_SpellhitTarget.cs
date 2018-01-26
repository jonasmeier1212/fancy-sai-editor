using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_SPELLHIT_TARGET
    /// 
    /// </summary>
    [Node(MenuName = "Spellhit target", Type = NodeType.EVENT_SPELLHIT_TARGET, AllowedTypes = new NodeType[] { NodeType.GENERAL_SPELL, NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class SpellhitTarget : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public SpellhitTarget()
        {
            Type = NodeType.EVENT_SPELLHIT_TARGET;

            EventId = "31";

            //Update text
            NodeName.Content = "Spellhit target";

            AddParam<GeneralNodes.Spell>(ParamId.PARAM_1, NodeType.GENERAL_SPELL, "Spell");
            AddParam(ParamId.PARAM_2, "School:"); //TODO: Enum for school selection
            AddParam(ParamId.PARAM_3, "Repeat min:");
            AddParam(ParamId.PARAM_4, "Repeat max:");
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new SpellhitTarget();
        }
    }
}
