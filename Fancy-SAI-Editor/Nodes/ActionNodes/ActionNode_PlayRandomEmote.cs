using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FancySaiEditor.Nodes.ActionNodes
{
    [Node(MenuName = "Play Random Emote", Type = NodeType.ACTION_PLAY_RANDOM_EMOTE, AllowedTypes = new NodeType[] { NodeType.PARAM_EMOTE, NodeType.EVENT, NodeType.TARGET })]
    class PlayRandomEmote : ActionNode
    {
        public PlayRandomEmote()
        {
            Type = NodeType.ACTION_PLAY_RANDOM_EMOTE;

            

            //Update text
            NodeName.Content = "Play Random Emote";

            AddParam<ParamNodes.Emote>(ParamId.PARAM_1, NodeType.PARAM_EMOTE, "Emote 1");
            AddParam<ParamNodes.Emote>(ParamId.PARAM_2, NodeType.PARAM_EMOTE, "Emote 2");
            AddParam<ParamNodes.Emote>(ParamId.PARAM_3, NodeType.PARAM_EMOTE, "Emote 3");
        }

        public override Node Clone()
        {
            return new PlayRandomEmote();
        }
    }
}
