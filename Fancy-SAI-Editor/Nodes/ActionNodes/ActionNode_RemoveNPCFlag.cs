using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.ActionNodes
{
    [Node(MenuName = "Remove NPC Flag", Type = NodeType.ACTION_REMOVE_NPC_FLAG, AllowedTypes = new NodeType[] { NodeType.EVENT, NodeType.TARGET })]
    class RemoveNpcFlag : ActionNode
    {
        public RemoveNpcFlag()
        {
            Type = NodeType.ACTION_REMOVE_NPC_FLAG;

            ActionId = "83";

            //Update text
            NodeName.Content = "Remove NPC Flag";

            AddParam<NpcFlags>(ParamId.PARAM_1, "Flag");
        }

        public override Node Clone()
        {
            return new RemoveNpcFlag();
        }
    }
}
