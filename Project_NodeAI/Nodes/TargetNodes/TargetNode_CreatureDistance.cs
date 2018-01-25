using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.TargetNodes
{
    [Node(MenuName = "Creature distance", Type = NodeType.TARGET_CREATURE_DISTANCE, AllowedTypes = new NodeType[] { NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class CreatureDistance : TargetNode
    {
        public CreatureDistance()
        {
            Type = NodeType.TARGET_CREATURE_DISTANCE;

            TargetId = "11";

            NodeName.Content = "Creature distance";

            AddParam<GeneralNodes.Npc>(ParamId.PARAM_1, NodeType.GENERAL_NPC, "NPC");
            AddParam(ParamId.PARAM_2, "Max Dist:");
        }

        public override Node Clone()
        {
            return new CreatureDistance();
        }
    }
}
