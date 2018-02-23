using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancySaiEditor.Nodes.TargetNodes
{
    [Node(MenuName = "Creature GUID", Type = NodeType.TARGET_CREATURE_GUID, AllowedTypes = new NodeType[] { NodeType.ACTION })]
    public class CreatureGUID : TargetNode
    {
        public CreatureGUID()
        {
            Type = NodeType.TARGET_CREATURE_GUID;

            

            NodeName.Content = "Creature GUID";

            AddParam(ParamId.PARAM_1, "GUID:");
            AddParam<GeneralNodes.Npc>(ParamId.PARAM_2, NodeType.GENERAL_NPC, "NPC");
        }

        public override Node Clone()
        {
            return new CreatureGUID();
        }
    }
}
