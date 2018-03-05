using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FancySaiEditor.Nodes.ActionNodes
{
    [Node(MenuName = "Simple talk", Type = NodeType.ACTION_SIMPLE_TALK, AllowedTypes = new NodeType[] { NodeType.PARAM_TEXT, NodeType.EVENT, NodeType.TARGET })]
    class SimpleTalk : ActionNode
    {
        public SimpleTalk()
        {
            Type = NodeType.ACTION_SIMPLE_TALK;

            

            //Update text
            NodeName.Content = "Simple talk";

            AddParam<ParamNodes.Text>(ParamId.PARAM_1, NodeType.PARAM_TEXT, "Text:");
        }

        public override Node Clone()
        {
            return new SimpleTalk();
        }
    }
}
