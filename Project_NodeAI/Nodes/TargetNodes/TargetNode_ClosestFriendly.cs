using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.TargetNodes
{
    [Node(MenuName = "Closest friendly", Type = NodeType.TARGET_SELF, AllowedTypes = new NodeType[] { NodeType.ACTION })]
    public class ClosestFriendly : TargetNode
    {
        public ClosestFriendly()
        {
            Type = NodeType.TARGET_SELF;

            TargetId = "26";

            NodeName.Content = "Closest friendly";

            AddParam(ParamId.PARAM_1, "Max dist:");
            AddParam<YesNo>(ParamId.PARAM_2, "Player only");
        }

        public override Node Clone()
        {
            return new ClosestFriendly();
        }
    }
}
