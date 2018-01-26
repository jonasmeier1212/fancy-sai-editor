using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.TargetNodes
{
    [Node(MenuName = "Hostile random not top", Type = NodeType.TARGET_HOSTILE_RANDOM_NOT_TOP, AllowedTypes = new NodeType[] { NodeType.ACTION })]
    public class HostileRandomNotTop : TargetNode
    {
        public HostileRandomNotTop()
        {
            Type = NodeType.TARGET_HOSTILE_RANDOM_NOT_TOP;

            TargetId = "6";

            NodeName.Content = "Hostile random not top";
        }

        public override Node Clone()
        {
            return new HostileRandomNotTop();
        }
    }
}
