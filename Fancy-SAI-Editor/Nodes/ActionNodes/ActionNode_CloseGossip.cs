using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.ActionNodes
{
    [Node(MenuName = "Close gossip", Type = NodeType.ACTION_CLOSE_GOSSIP, AllowedTypes = new NodeType[] { NodeType.EVENT, NodeType.TARGET })]
    class CloseGossip : ActionNode
    {
        public CloseGossip()
        {
            Type = NodeType.ACTION_CLOSE_GOSSIP;

            ActionId = "72";

            //Update text
            NodeName.Content = "Close gossip";
        }

        public override Node Clone()
        {
            return new CloseGossip();
        }
    }
}
