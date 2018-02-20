using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NodeAI.Nodes.ActionNodes
{
    [Node(MenuName = "Fail Quest", Type = NodeType.ACTION_FAIL_QUEST, AllowedTypes = new NodeType[] { NodeType.GENERAL_QUEST, NodeType.EVENT, NodeType.TARGET })]
    class FailQuest : ActionNode
    {
        public FailQuest()
        {
            Type = NodeType.ACTION_FAIL_QUEST;

            

            //Update text
            NodeName.Content = "Fail Quest";

            AddParam<GeneralNodes.Quest>(ParamId.PARAM_1, NodeType.GENERAL_QUEST, "Quest");
        }

        public override Node Clone()
        {
            return new FailQuest();
        }
    }
}
