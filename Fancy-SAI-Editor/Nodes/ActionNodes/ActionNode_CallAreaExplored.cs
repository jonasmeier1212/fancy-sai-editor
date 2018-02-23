using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FancySaiEditor.Nodes.ActionNodes
{
    [Node(MenuName = "CallAreaExplored", Type = NodeType.ACTION_CALL_AREAEXPLORED, AllowedTypes = new NodeType[] { NodeType.EVENT, NodeType.TARGET })]
    class CallAreaExplored : ActionNode
    {
        public CallAreaExplored()
        {
            Type = NodeType.ACTION_CALL_AREAEXPLORED;

            //Update text
            NodeName.Content = "Call Area Explored";

            AddParam<GeneralNodes.Quest>(ParamId.PARAM_1, NodeType.GENERAL_QUEST, "Quest");
        }

        public override Node Clone()
        {
            return new CallAreaExplored();
        }
    }
}
