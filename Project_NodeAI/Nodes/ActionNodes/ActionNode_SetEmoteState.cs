using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NodeAI.Nodes.ActionNodes
{
    [Node(MenuName = "Set Emote State", Type = NodeType.ACTION_SET_EMOTE_STATE, AllowedTypes = new NodeType[] {NodeType.GENERAL_EMOTE, NodeType.EVENT, NodeType.TARGET })]
    class SetEmoteState : ActionNode
    {
        public SetEmoteState()
        {
            Type = NodeType.ACTION_SET_EMOTE_STATE;

            ActionId = "17";

            //Update text
            NodeName.Content = "Set Emote State";

            AddParam<GeneralNodes.Emote>(ParamId.PARAM_1, NodeType.GENERAL_EMOTE, "Emote");
        }

        public override Node Clone()
        {
            return new SetEmoteState();
        }
    }
}
