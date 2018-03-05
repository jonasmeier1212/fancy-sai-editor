using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancySaiEditor.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_SUMMONED_UNIT
    /// 
    /// </summary>
    [Node(MenuName = "Summoned Unit", Type = NodeType.EVENT_SUMMONED_UNIT, AllowedTypes = new NodeType[] { NodeType.AI_OWNER, NodeType.ACTION })]
    public class SummonedUnit : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public SummonedUnit()
        {
            Type = NodeType.EVENT_SUMMONED_UNIT;

            

            //Update text
            NodeName.Content = "Summoned Unit";
            AddParam<ParamNodes.Npc>(ParamId.PARAM_1, NodeType.PARAM_NPC, "Creature");
            AddParam(ParamId.PARAM_2, "Cooldown min");
            AddParam(ParamId.PARAM_3, "Cooldown max");
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new SummonedUnit();
        }
    }
}
