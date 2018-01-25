using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NodeAI.Nodes.ActionNodes
{
    [Node(MenuName = "Follow", Type = NodeType.ACTION_FOLLOW, AllowedTypes = new NodeType[] { NodeType.EVENT, NodeType.TARGET })]
    class Follow : ActionNode
    {
        public Follow()
        {
            Type = NodeType.ACTION_FOLLOW;

            ActionId = "29";

            //Update text
            NodeName.Content = "Follow";

            AddParam(ParamId.PARAM_1, "Distance");
            AddParam(ParamId.PARAM_2, "Angle");
            AddParam<GeneralNodes.Npc>(ParamId.PARAM_3, NodeType.GENERAL_NPC, "End NPC");
            // TODO: Remain params
        }

        public override Node Clone()
        {
            return new Follow();
        }
    }
}
