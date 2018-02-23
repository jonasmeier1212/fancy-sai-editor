using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FancySaiEditor.Nodes.ActionNodes
{
    [Node( MenuName = "Talk", Type = NodeType.ACTION_TALK, AllowedTypes = new NodeType[] { NodeType.GENERAL_TEXT, NodeType.EVENT })]
    class Talk : ActionNode
    {
        public Talk()
        {
            Type = NodeType.ACTION_TALK;

            

            //Update text
            NodeName.Content = "Talk";

            AddParam<GeneralNodes.Text>(ParamId.PARAM_1, NodeType.GENERAL_TEXT, "Text");
        }

        public override Node Clone()
        {
            return new Talk();
        }
    }
}
