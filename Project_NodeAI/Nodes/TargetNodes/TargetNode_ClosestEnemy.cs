using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.TargetNodes
{
    [Node(MenuName = "Closest enemy", Type = NodeType.TARGET_CLOSEST_ENEMY, AllowedTypes = new NodeType[] { NodeType.ACTION })]
    public class ClosestEnemy : TargetNode
    {
        public ClosestEnemy()
        {
            Type = NodeType.TARGET_CLOSEST_ENEMY;

            TargetId = "26";

            NodeName.Content = "Closest enemy";

            AddParam(ParamId.PARAM_1, "Max dist:");
            AddParam<YesNo>(ParamId.PARAM_2, "Player only");
        }

        public override Node Clone()
        {
            return new ClosestEnemy();
        }
    }
}
