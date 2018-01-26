using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NodeAI.Nodes.EventNodes
{
    [Node(MenuName = "Quest Accepted", Type = NodeType.EVENT_QUEST_ACCEPT, AllowedTypes = new NodeType[] { NodeType.GENERAL_QUEST, NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class QuestAccepted : EventNode
    {
        public QuestAccepted()
        {
            Type = NodeType.EVENT_QUEST_ACCEPT;

            EventId = "19";
            NodeName.Content = "Quest Accepted";

            AddParam<GeneralNodes.Quest>(ParamId.PARAM_3, NodeType.GENERAL_QUEST, "Quest");
        }

        enum TestEnum
        {
            ITEM_1,
            ITEM_2,
            ITEM_3,
        }

        public override Node Clone()
        {
            return new QuestAccepted();
        }
    }
}
