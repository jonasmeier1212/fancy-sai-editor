using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_VICTIM_CASTING
    /// 
    /// </summary>
    [Node(MenuName = "Victim casting", Type = NodeType.EVENT_VICTIM_CASTING, AllowedTypes = new NodeType[] { NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class VictimCasting : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public VictimCasting()
        {
            Type = NodeType.EVENT_VICTIM_CASTING;

            //Update text
            NodeName.Content = "Victim casting";

            AddParam(ParamId.PARAM_1, "Repeat min");
            AddParam(ParamId.PARAM_2, "Repeat max");
            AddParam<GeneralNodes.Spell>(ParamId.PARAM_3, NodeType.GENERAL_SPELL, "Spell");
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new VictimCasting();
        }
    }
}
