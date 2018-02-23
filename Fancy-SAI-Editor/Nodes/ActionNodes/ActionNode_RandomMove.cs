using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FancySaiEditor.Nodes.ActionNodes
{
    [Node(MenuName = "Random move", Type = NodeType.ACTION_RANDOM_MOVE, AllowedTypes = new NodeType[] { NodeType.EVENT, NodeType.TARGET })]
    class RandomMove : ActionNode
    {
        public RandomMove()
        {
            Type = NodeType.ACTION_RANDOM_MOVE;

            

            //Update text
            NodeName.Content = "Random move";

            AddParam(ParamId.PARAM_1, "Radius:");
        }

        public override Node Clone()
        {
            return new RandomMove();
        }
    }
}
