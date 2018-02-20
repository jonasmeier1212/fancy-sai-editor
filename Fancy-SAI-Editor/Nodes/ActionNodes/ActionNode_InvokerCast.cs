using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NodeAI.Nodes.ActionNodes
{
    [Node(MenuName = "Invoker cast", Type = NodeType.ACTION_CAST, AllowedTypes = new NodeType[] { NodeType.GENERAL_SPELL, NodeType.EVENT, NodeType.TARGET })]
    class InvokerCast : ActionNode
    {
        public InvokerCast()
        {
            Type = NodeType.ACTION_CAST;

            

            //Update text
            NodeName.Content = "Invoker cast";

            AddParam<GeneralNodes.Spell>(ParamId.PARAM_1, NodeType.GENERAL_SPELL, "Spell");
            AddParam<CastFlags>(ParamId.PARAM_2, "Flags:");
            AddParam(ParamId.PARAM_3, "Triggered flags:"); //TODO: Enum for this
        }

        public override Node Clone()
        {
            return new InvokerCast();
        }
    }
}
