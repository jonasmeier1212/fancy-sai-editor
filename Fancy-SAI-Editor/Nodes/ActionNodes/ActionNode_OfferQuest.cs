using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NodeAI.Nodes.ActionNodes
{
    [Node(MenuName = "Offer Quest", Type = NodeType.ACTION_OFFER_QUEST, AllowedTypes = new NodeType[] { NodeType.GENERAL_QUEST, NodeType.EVENT, NodeType.TARGET })]
    class OfferQuest : ActionNode
    {
        public OfferQuest()
        {
            Type = NodeType.ACTION_OFFER_QUEST;

            ActionId = "7";

            //Update text
            NodeName.Content = "Offer Quest";

            AddParam<GeneralNodes.Quest>(ParamId.PARAM_1, NodeType.GENERAL_QUEST, "Quest");
            AddParam<DirectAdd>(ParamId.PARAM_2, "Direct add");
        }

        enum DirectAdd
        {
            NO,
            YES,
        }

        public override Node Clone()
        {
            return new OfferQuest();
        }
    }
}
