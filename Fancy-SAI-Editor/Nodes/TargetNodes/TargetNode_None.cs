using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancySaiEditor.Nodes.TargetNodes
{
    [Node(MenuName = "None", Type = NodeType.TARGET_NONE, AllowedTypes = new NodeType[] { NodeType.ACTION })]
    public class None : TargetNode
    {
        public None()
        {
            Type = NodeType.TARGET_NONE;

            NodeName.Content = "None";
        }

        public override Node Clone()
        {
            return new None();
        }
    }
}
