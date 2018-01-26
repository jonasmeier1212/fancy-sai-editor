using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NodeAI.Nodes.ActionNodes
{
    [Node(MenuName = "ActivateGObject", Type = NodeType.ACTION_ACTIVATE_GOBJECT, AllowedTypes = new NodeType[] { NodeType.EVENT, NodeType.TARGET })]
    class ActivateGObject : ActionNode
    {
        public ActivateGObject()
        {
            Type = NodeType.ACTION_ACTIVATE_GOBJECT;

            //Update text
            NodeName.Content = "Activate gameobject";
        }

        public override Node Clone()
        {
            return new ActivateGObject();
        }
    }
}
