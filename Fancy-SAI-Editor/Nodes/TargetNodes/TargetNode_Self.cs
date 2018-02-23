using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancySaiEditor.Nodes.TargetNodes
{
    [Node(MenuName = "Self", Type = NodeType.TARGET_SELF, AllowedTypes = new NodeType[] { NodeType.ACTION })]
    public class Self : TargetNode
    {
        public Self()
        {
            Type = NodeType.TARGET_SELF;

            

            NodeName.Content = "Self";
        }

        public override Node Clone()
        {
            return new Self();
        }
    }
}
