using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NodeAI.Nodes.ActionNodes
{
    [Node(MenuName = "Dynamic Flag", Type = NodeType.ACTION_DYNAMIC_FLAG, AllowedTypes = new NodeType[] { NodeType.EVENT, NodeType.TARGET })]
    class DynamicFlag : ActionNode
    {
        public DynamicFlag()
        {
            Type = NodeType.ACTION_DYNAMIC_FLAG;

            //Update text
            NodeName.Content = "Dynamic Flag";

            AddParam<DynamicFlagSpecificType>(ParamId.PARAM_SPECIFIC_TYPE, "Type:");
            AddParam<DynamicFlags>(ParamId.PARAM_1, "Flag");
        }

        enum DynamicFlagSpecificType
        {
            SET_DYNAMIC_FLAG    = 94,
            ADD_DYNAMIC_FLAG    = 95,
            REMOVE_DYNAMIC_FLAG = 96,
        }

        public override Node Clone()
        {
            return new DynamicFlag();
        }
    }
}
