using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FancySaiEditor.Nodes.EventNodes
{
    [Node(MenuName = "Quest reward", Type = NodeType.EVENT_QUEST_REWARD, AllowedTypes = new NodeType[] { NodeType.GENERAL_QUEST, NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class QuestReward : EventNode
    {
        public QuestReward()
        {
            Type = NodeType.EVENT_QUEST_REWARD;

            NodeName.Content = "Quest reward";

            AddParam<GeneralNodes.Quest>(ParamId.PARAM_3, NodeType.GENERAL_QUEST, "Quest");
        }

        public override Node Clone()
        {
            return new QuestReward();
        }
    }
}
