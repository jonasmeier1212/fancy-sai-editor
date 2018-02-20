using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NodeAI.Nodes.ActionNodes
{
    [Node(MenuName = "CombatStop", Type = NodeType.ACTION_COMBAT_STOP, AllowedTypes = new NodeType[] { NodeType.EVENT, NodeType.TARGET })]
    class CombatStop : ActionNode
    {
        public CombatStop()
        {
            Type = NodeType.ACTION_COMBAT_STOP;

            

            //Update text
            NodeName.Content = "CombatStop";
        }

        public override Node Clone()
        {
            return new CombatStop();
        }
    }
}
