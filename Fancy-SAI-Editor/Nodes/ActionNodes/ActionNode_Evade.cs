using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NodeAI.Nodes.ActionNodes
{
    [Node(MenuName = "Evade", Type = NodeType.ACTION_EVADE, AllowedTypes = new NodeType[] { NodeType.EVENT, NodeType.TARGET })]
    class Evade : ActionNode
    {
        public Evade()
        {
            Type = NodeType.ACTION_EVADE;

            

            //Update text
            NodeName.Content = "Evade";
        }

        public override Node Clone()
        {
            return new Evade();
        }
    }
}
