using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.TargetNodes
{
    [Node(MenuName = "Hostile second aggro", Type = NodeType.TARGET_HOSTILE_SECOND_AGGRO, AllowedTypes = new NodeType[] { NodeType.ACTION })]
    public class HostileSecondAggro : TargetNode
    {
        public HostileSecondAggro()
        {
            Type = NodeType.TARGET_HOSTILE_SECOND_AGGRO;

            TargetId = "3";

            NodeName.Content = "Hostile second aggro";
        }

        public override Node Clone()
        {
            return new HostileSecondAggro();
        }
    }
}
