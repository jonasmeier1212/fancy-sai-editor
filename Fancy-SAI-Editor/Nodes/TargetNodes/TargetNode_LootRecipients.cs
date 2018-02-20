using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.TargetNodes
{
    [Node(MenuName = "Loot recipients", Type = NodeType.TARGET_LOOT_RECIPIENTS, AllowedTypes = new NodeType[] { NodeType.ACTION })]
    public class LootRecipients : TargetNode
    {
        public LootRecipients()
        {
            Type = NodeType.TARGET_LOOT_RECIPIENTS;

            

            NodeName.Content = "Loot recipients";
        }

        public override Node Clone()
        {
            return new LootRecipients();
        }
    }
}
