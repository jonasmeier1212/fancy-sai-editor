using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NodeAI.Nodes.ActionNodes
{
    [Node(MenuName = "Set React State", Type = NodeType.ACTION_SET_REACT_STATE, AllowedTypes = new NodeType[] { NodeType.EVENT, NodeType.TARGET })]
    class SetReactState : ActionNode
    {
        public SetReactState()
        {
            Type = NodeType.ACTION_SET_REACT_STATE;

            ActionId = "8";

            //Update text
            NodeName.Content = "Set React State";

            AddParam<ReactState>(ParamId.PARAM_1, "React State");
        }

        public override Node Clone()
        {
            return new SetReactState();
        }

        enum ReactState
        {
            PASSIVE     = 0,
            DEFENSIVE   = 1,
            AGGRESSIVE  = 2,
            ASSIST      = 3
        };
    }
}
