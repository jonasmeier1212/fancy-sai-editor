using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.TargetNodes
{
    [Node(MenuName = "Hostile random", Type = NodeType.TARGET_HOSTILE_RANDOM, AllowedTypes = new NodeType[] { NodeType.ACTION })]
    public class HostileRandom : TargetNode
    {
        public HostileRandom()
        {
            Type = NodeType.TARGET_HOSTILE_RANDOM;

            TargetId = "5";

            NodeName.Content = "Hostile random";
        }

        public override Node Clone()
        {
            return new HostileRandom();
        }
    }
}
