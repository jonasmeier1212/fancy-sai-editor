using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.ActionNodes
{
    [Node(MenuName = "Kill Unit", Type = NodeType.ACTION_KILL_UNIT, AllowedTypes = new NodeType[] { NodeType.EVENT, NodeType.TARGET })]
    class KillUnit : ActionNode
    {
        public KillUnit()
        {
            Type = NodeType.ACTION_PLAY_SOUND;

            

            //Update text
            NodeName.Content = "Kill Unit";
        }

        public override Node Clone()
        {
            return new KillUnit();
        }
    }
}
