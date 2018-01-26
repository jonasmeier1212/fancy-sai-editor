using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NodeAI.Nodes.EventNodes
{
    [Node(MenuName = "Quest", Type = NodeType.EVENT_QUEST, AllowedTypes = new NodeType[] { NodeType.GENERAL_QUEST, NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class Quest : EventNode
    {
        public Quest()
        {
            Type = NodeType.EVENT_QUEST;

            NodeName.Content = "Quest";

            AddParam<QuestEventTypes>(ParamId.PARAM_SPECIFIC_TYPE, "Type:");
            AddParam<GeneralNodes.Quest>(ParamId.PARAM_3, NodeType.GENERAL_QUEST, "Quest");
        }

        enum QuestEventTypes
        {
            ACCEPT = 19,
            REWARD = 20,
        }

        public override Node Clone()
        {
            return new Quest();
        }
    }
}
