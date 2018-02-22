using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NodeAI.Nodes.ActionNodes
{
    [Node(MenuName = "Move forward", Type = NodeType.ACTION_MOVE_FORWARD, AllowedTypes = new NodeType[] { NodeType.EVENT, NodeType.TARGET })]
    class MoveForward : ActionNode
    { 
        public MoveForward()
        {
            Type = NodeType.ACTION_MOVE_FORWARD;

            //Update text
            NodeName.Content = "Move forward";

            AddParam(ParamId.PARAM_1, "Distance in yard:");
        }

        public override Node Clone()
        {
            return new MoveForward();
        }
    }
}
