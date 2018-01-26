using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NodeAI.Nodes.ActionNodes
{
    [Node(MenuName = "Play Emote", Type = NodeType.ACTION_PLAY_EMOTE, AllowedTypes = new NodeType[] { NodeType.GENERAL_EMOTE, NodeType.EVENT, NodeType.TARGET })]
    class PlayEmote : ActionNode
    {
        public PlayEmote()
        {
            Type = NodeType.ACTION_PLAY_EMOTE;

            ActionId = "5";

            //Update text
            NodeName.Content = "Play Emote";

            AddParam<GeneralNodes.Emote>(ParamId.PARAM_1, NodeType.GENERAL_EMOTE, "Emote");
        }

        public override Node Clone()
        {
            return new PlayEmote();
        }
    }
}
