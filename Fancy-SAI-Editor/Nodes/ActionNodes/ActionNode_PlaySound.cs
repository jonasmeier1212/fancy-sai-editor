using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FancySaiEditor.Nodes.ActionNodes
{
    [Node(MenuName = "Play Sound", Type = NodeType.ACTION_PLAY_SOUND, AllowedTypes = new NodeType[] { NodeType.EVENT, NodeType.TARGET })]
    class PlaySound : ActionNode
    {
        public PlaySound()
        {
            Type = NodeType.ACTION_PLAY_SOUND;

            

            //Update text
            NodeName.Content = "Play Sound";

            AddParam<ParamNodes.Sound>(ParamId.PARAM_1, NodeType.PARAM_SOUND, "Sound");
            AddParam<OnlySelf>(ParamId.PARAM_2, "Only self");
            AddParam<DistantSound>(ParamId.PARAM_3, "Distant sound");
        }

        enum OnlySelf
        {
            NO,
            YES,
        }

        enum DistantSound
        {
            NO,
            YES,
        }

        public override Node Clone()
        {
            return new PlaySound();
        }
    }
}
