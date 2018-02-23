using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancySaiEditor.Nodes.ActionNodes
{
    [Node(MenuName = "Attack start", Type = NodeType.ACTION_ATTACK_START, AllowedTypes = new NodeType[] { NodeType.EVENT, NodeType.TARGET })]
    class AttackStart : ActionNode
    {
        public AttackStart()
        {
            Type = NodeType.ACTION_ATTACK_START;

            

            //Update text
            NodeName.Content = "Attack start";
        }

        public override Node Clone()
        {
            return new AttackStart();
        }
    }
}
