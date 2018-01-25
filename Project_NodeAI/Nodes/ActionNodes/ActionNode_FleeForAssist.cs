using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NodeAI.Nodes.ActionNodes
{
    [Node(MenuName = "Flee for assist", Type = NodeType.ACTION_FLEE_FOR_ASSIST, AllowedTypes = new NodeType[] { NodeType.EVENT, NodeType.TARGET })]
    class FleeForAssist : ActionNode
    {
        public FleeForAssist()
        {
            Type = NodeType.ACTION_FLEE_FOR_ASSIST;

            ActionId = "25";

            //Update text
            NodeName.Content = "Flee for assist";

            AddParam<SayFleeText>(ParamId.PARAM_1, "Say Flee text");
        }

        enum SayFleeText
        {
            NO,
            YES,
        }

        public override Node Clone()
        {
            return new FleeForAssist();
        }
    }
}
