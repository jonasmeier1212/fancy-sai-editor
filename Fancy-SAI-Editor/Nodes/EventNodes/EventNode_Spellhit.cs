using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_SPELLHIT
    ///
    /// </summary>
    [Node(MenuName = "Spellhit", Type = NodeType.EVENT_SPELLHIT, AllowedTypes = new NodeType[] { NodeType.GENERAL_SPELL, NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class Spellhit : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public Spellhit()
        {
            Type = NodeType.EVENT_SPELLHIT;

            

            //Update text
            NodeName.Content = "Spellhit";
            AddParam<GeneralNodes.Spell>(ParamId.PARAM_1, NodeType.GENERAL_SPELL, "Spell");
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new Spellhit();
        }
    }
}
