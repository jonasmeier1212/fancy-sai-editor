using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancySaiEditor.Nodes.TargetNodes
{
    [Node(MenuName = "Creature range", Type = NodeType.TARGET_CREATURE_RANGE, AllowedTypes = new NodeType[] { NodeType.PARAM_NPC, NodeType.ACTION })]
    public class CreatureRange : TargetNode
    {
        public CreatureRange()
        {
            Type = NodeType.TARGET_CREATURE_RANGE;

            

            NodeName.Content = "Creature range";

            AddParam<ParamNodes.Npc>(ParamId.PARAM_1, NodeType.PARAM_NPC, "Creature");
            AddParam(ParamId.PARAM_2, "Min Dist:");
            AddParam(ParamId.PARAM_3, "Max Dist:");
        }

        public override Node Clone()
        {
            return new CreatureRange();
        }
    }
}
