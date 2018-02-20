using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NodeAI.Nodes.ActionNodes
{
    [Node(MenuName = "Add dynamic flag", Type = NodeType.ACTION_ADD_DYNAMIC_FLAG, AllowedTypes = new NodeType[] { NodeType.EVENT, NodeType.TARGET })]
    class AddDynamicFlag : ActionNode
    {
        public AddDynamicFlag()
        {
            Type = NodeType.ACTION_ADD_DYNAMIC_FLAG;

            //Update text
            NodeName.Content = "Add dynamic flag";

            AddParam<DynamicFlags>(ParamId.PARAM_1, "Flag");
        }

        public override Node Clone()
        {
            return new AddDynamicFlag();
        }
    }
}
