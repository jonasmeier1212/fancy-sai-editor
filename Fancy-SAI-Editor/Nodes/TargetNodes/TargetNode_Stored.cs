using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.TargetNodes
{
    [Node(MenuName = "Stored", Type = NodeType.TARGET_STORED, AllowedTypes = new NodeType[] { NodeType.ACTION })]
    public class Stored : TargetNode
    {
        public Stored()
        {
            Type = NodeType.TARGET_STORED;

            TargetId = "12";

            NodeName.Content = "Stored";

            AddParam(ParamId.PARAM_1, "Stored Target ID");
        }

        public override Node Clone()
        {
            return new Stored();
        }
    }
}
