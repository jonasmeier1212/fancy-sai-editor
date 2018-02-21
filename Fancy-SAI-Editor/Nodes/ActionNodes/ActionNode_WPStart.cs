using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NodeAI.Nodes.ActionNodes
{
    [Node(MenuName = "Waypoint Start", Type = NodeType.ACTION_WP_START, AllowedTypes = new NodeType[] { NodeType.GENERAL_QUEST, NodeType.EVENT, NodeType.TARGET })]
    class WPStart : ActionNode
    {
        public WPStart()
        {
            Type = NodeType.ACTION_WP_START;

            //Update text
            NodeName.Content = "Waypoint Start";

            AddParam<WalkRun>(ParamId.PARAM_1, "");
            AddParam(ParamId.PARAM_2, "Waypoint Entry:"); //TODO: Replace with general node
            AddParam<YesNo>(ParamId.PARAM_3, "Can repeat?");
            AddParam<GeneralNodes.Quest>(ParamId.PARAM_4, NodeType.GENERAL_QUEST, "Quest");
            AddParam(ParamId.PARAM_5, "Despawntime:");
            AddParam<ReactState>(ParamId.PARAM_6, "React state:");
        }

        enum WalkRun
        {
            Walk,
            Run,
        }

        enum ReactState
        {
            Passive = 0,
            Defensive = 1,
            Aggresive = 2,
            Assist = 3
        };

        public override Node Clone()
        {
            return new WPStart();
        }
    }
}
