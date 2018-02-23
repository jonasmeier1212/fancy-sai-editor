using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FancySaiEditor.Nodes.ActionNodes
{
    [Node(MenuName = "Increase event phase", Type = NodeType.ACTION_INC_EVENT_PHASE, AllowedTypes = new NodeType[] { NodeType.EVENT, NodeType.TARGET })]
    class IncEventPhase : ActionNode
    {
        public IncEventPhase()
        {
            Type = NodeType.ACTION_INC_EVENT_PHASE;

            

            //Update text
            NodeName.Content = "Increase event phase";

            AddParam(ParamId.PARAM_1, "Increment");
            AddParam(ParamId.PARAM_2, "Decrement");
        }

        public override Node Clone()
        {
            return new IncEventPhase();
        }
    }
}
