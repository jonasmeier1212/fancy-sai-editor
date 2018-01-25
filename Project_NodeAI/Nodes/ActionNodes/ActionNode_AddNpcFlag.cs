using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.ActionNodes
{
    [Node(MenuName = "Add NPC Flag", Type = NodeType.ACTION_ADD_NPC_FLAG, AllowedTypes = new NodeType[] { NodeType.EVENT, NodeType.TARGET })]
    class AddNpcFlag : ActionNode
    {
        public AddNpcFlag()
        {
            Type = NodeType.ACTION_ADD_NPC_FLAG;

            ActionId = "82";

            //Update text
            NodeName.Content = "Add NPC Flag";

            AddParam<NpcFlags>(ParamId.PARAM_1, "Flag");
        }

        public override Node Clone()
        {
            return new AddNpcFlag();
        }
    }
}
