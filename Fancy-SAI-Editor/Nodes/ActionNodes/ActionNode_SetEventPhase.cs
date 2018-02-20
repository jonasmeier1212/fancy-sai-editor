using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NodeAI.Nodes.ActionNodes
{
    [Node(MenuName = "SetEventPhase", Type = NodeType.ACTION_SET_EVENT_PHASE, AllowedTypes = new NodeType[] { NodeType.EVENT, NodeType.TARGET })]
    class SetEventPhase : ActionNode
    {
        public SetEventPhase()
        {
            Type = NodeType.ACTION_SET_EVENT_PHASE;

            

            //Update text
            NodeName.Content = "SetEventPhase";

            AddParam<SmartEventPhases>(ParamId.PARAM_1, "Phase");
        }

        [Flags]
        enum SmartEventPhases
        {
            PHASE_1,
            PHASE_2,
            PHASE_3,
            PHASE_4,
            PHASE_5,
            PHASE_6,
            PHASE_7,
            PHASE_8,
            PHASE_9,
        }

        public override Node Clone()
        {
            return new SetEventPhase();
        }
    }
}
