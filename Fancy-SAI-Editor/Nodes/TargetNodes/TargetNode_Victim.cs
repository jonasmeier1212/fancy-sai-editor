using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.TargetNodes
{
    [Node(MenuName = "Victim", Type = NodeType.TARGET_VICTIM, AllowedTypes = new NodeType[] { NodeType.ACTION })]
    public class Victim : TargetNode
    {
        public Victim()
        {
            Type = NodeType.TARGET_VICTIM;

            

            NodeName.Content = "Victim";
        }

        public override Node Clone()
        {
            return new Victim();
        }
    }
}
