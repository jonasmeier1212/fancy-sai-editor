using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.TargetNodes
{
    [Node(MenuName = "Creature range", Type = NodeType.TARGET_CREATURE_RANGE, AllowedTypes = new NodeType[] { NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class CreatureRange : TargetNode
    {
        public CreatureRange()
        {
            Type = NodeType.TARGET_CREATURE_RANGE;

            TargetId = "9";

            NodeName.Content = "Creature range";

            AddParam<GeneralNodes.Npc>(ParamId.PARAM_1, NodeType.GENERAL_NPC, "Creature");
            AddParam(ParamId.PARAM_2, "Min Dist:");
            AddParam(ParamId.PARAM_3, "Max Dist:");
        }

        public override Node Clone()
        {
            return new CreatureRange();
        }
    }
}
