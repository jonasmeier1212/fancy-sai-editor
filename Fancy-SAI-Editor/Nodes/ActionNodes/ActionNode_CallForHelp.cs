using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FancySaiEditor.Nodes.ActionNodes
{
    [Node(MenuName = "Call for help", Type = NodeType.ACTION_CALL_FOR_HELP, AllowedTypes = new NodeType[] { NodeType.PARAM_SPELL, NodeType.EVENT, NodeType.TARGET })]
    class CallForHelp : ActionNode
    {
        public CallForHelp()
        {
            Type = NodeType.ACTION_CALL_FOR_HELP;

            //Update text
            NodeName.Content = "Call for help";

            AddParam(ParamId.PARAM_1, "Radius:");
            AddParam<YesNo>(ParamId.PARAM_2, "Say call for help text:");
        }

        public override Node Clone()
        {
            return new CallForHelp();
        }
    }
}
