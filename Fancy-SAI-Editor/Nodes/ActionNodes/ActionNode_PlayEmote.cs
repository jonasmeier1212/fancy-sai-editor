using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FancySaiEditor.Nodes.ActionNodes
{
    [Node(MenuName = "Play Emote", Type = NodeType.ACTION_PLAY_EMOTE, AllowedTypes = new NodeType[] { NodeType.PARAM_EMOTE, NodeType.EVENT, NodeType.TARGET })]
    class PlayEmote : ActionNode
    {
        public PlayEmote()
        {
            Type = NodeType.ACTION_PLAY_EMOTE;

            

            //Update text
            NodeName.Content = "Play Emote";

            AddParam<ParamNodes.Emote>(ParamId.PARAM_1, NodeType.PARAM_EMOTE, "Emote");
        }

        public override Node Clone()
        {
            return new PlayEmote();
        }
    }
}
