using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.TargetNodes
{
    [Node(MenuName = "Farthest", Type = NodeType.TARGET_FARTHEST, AllowedTypes = new NodeType[] { NodeType.ACTION })]
    public class Farthest : TargetNode
    {
        public Farthest()
        {
            Type = NodeType.TARGET_FARTHEST;

            TargetId = "28";

            NodeName.Content = "Farthest";

            AddParam(ParamId.PARAM_1, "Max dist:");
            AddParam<YesNo>(ParamId.PARAM_2, "Only players?");
            AddParam<YesNo>(ParamId.PARAM_3, "In line of sight?");
        }

        public override Node Clone()
        {
            return new Farthest();
        }
    }
}
