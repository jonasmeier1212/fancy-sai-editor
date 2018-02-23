using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancySaiEditor.Nodes.TargetNodes
{
    [Node(MenuName = "Hostile last aggro", Type = NodeType.TARGET_HOSTILE_LAST_AGGRO, AllowedTypes = new NodeType[] { NodeType.ACTION })]
    public class HostileLastAggro : TargetNode
    {
        public HostileLastAggro()
        {
            Type = NodeType.TARGET_HOSTILE_LAST_AGGRO;

            

            NodeName.Content = "Hostile last aggro";
        }

        public override Node Clone()
        {
            return new HostileLastAggro();
        }
    }
}
