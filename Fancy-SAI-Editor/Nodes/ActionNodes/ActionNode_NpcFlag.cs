using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.ActionNodes
{
    [Node(MenuName = "NPC Flag", Type = NodeType.ACTION_NPC_FLAG, AllowedTypes = new NodeType[] { NodeType.EVENT, NodeType.TARGET })]
    class AddNpcFlag : ActionNode
    {
        public AddNpcFlag()
        {
            Type = NodeType.ACTION_NPC_FLAG;

            ActionId = "82";

            //Update text
            NodeName.Content = "NPC Flag";

            AddParam<NpcFlagSpecificType>(ParamId.PARAM_SPECIFIC_TYPE, "Type:");
            AddParam<NpcFlags>(ParamId.PARAM_1, "Flag:");
        }

        enum NpcFlagSpecificType
        {
            ADD_NPC_FLAG    = 82,
            SET_NPC_FLAG    = 81,
            REMOVE_NPC_FLAG = 83,
        }

        public override Node Clone()
        {
            return new AddNpcFlag();
        }
    }
}
