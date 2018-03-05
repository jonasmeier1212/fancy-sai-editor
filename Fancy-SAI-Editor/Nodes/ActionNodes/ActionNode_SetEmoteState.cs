using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FancySaiEditor.Nodes.ActionNodes
{
    [Node(MenuName = "Set Emote State", Type = NodeType.ACTION_SET_EMOTE_STATE, AllowedTypes = new NodeType[] {NodeType.PARAM_EMOTE, NodeType.EVENT, NodeType.TARGET })]
    class SetEmoteState : ActionNode
    {
        public SetEmoteState()
        {
            Type = NodeType.ACTION_SET_EMOTE_STATE;

            

            //Update text
            NodeName.Content = "Set Emote State";

            AddParam<ParamNodes.Emote>(ParamId.PARAM_1, NodeType.PARAM_EMOTE, "Emote");
        }

        public override Node Clone()
        {
            return new SetEmoteState();
        }
    }
}
