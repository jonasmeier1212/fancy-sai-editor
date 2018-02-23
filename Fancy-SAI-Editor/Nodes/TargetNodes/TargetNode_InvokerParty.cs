using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancySaiEditor.Nodes.TargetNodes
{
    [Node(MenuName = "Invoker party", Type = NodeType.TARGET_INVOKER_PARTY, AllowedTypes = new NodeType[] { NodeType.ACTION })]
    public class InvokerParty : TargetNode
    {
        public InvokerParty()
        {
            Type = NodeType.TARGET_INVOKER_PARTY;

            

            NodeName.Content = "Invoker party";
        }

        public override Node Clone()
        {
            return new InvokerParty();
        }
    }
}
