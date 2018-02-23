using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FancySaiEditor.Nodes.ActionNodes
{
    [Node(MenuName = "Reset Gameobject", Type = NodeType.ACTION_RESET_GOBJECT, AllowedTypes = new NodeType[] { NodeType.EVENT, NodeType.TARGET })]
    class ResetGObject : ActionNode
    {
        public ResetGObject()
        {
            Type = NodeType.ACTION_RESET_GOBJECT;

            

            //Update text
            NodeName.Content = "Reset Gameobject";
        }

        public override Node Clone()
        {
            return new ResetGObject();
        }
    }
}
