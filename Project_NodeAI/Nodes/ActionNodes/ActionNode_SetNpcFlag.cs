using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.ActionNodes
{
    [Node(MenuName = "Set NPC Flag", Type = NodeType.ACTION_SET_NPC_FLAG, AllowedTypes = new NodeType[] { NodeType.EVENT, NodeType.TARGET })]
    class SetNpcFlag : ActionNode
    {
        public SetNpcFlag()
        {
            Type = NodeType.ACTION_SET_NPC_FLAG;

            ActionId = "81";

            //Update text
            NodeName.Content = "Set NPC Flag";

            AddParam<NpcFlags>(ParamId.PARAM_1, "Flag");
        }

        public override Node Clone()
        {
            return new SetNpcFlag();
        }
    }
}
