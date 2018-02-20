using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.TargetNodes
{
    [Node(MenuName = "Player distance", Type = NodeType.TARGET_PLAYER_DISTANCE, AllowedTypes = new NodeType[] { NodeType.ACTION })]
    public class PlayerDistance : TargetNode
    {
        public PlayerDistance()
        {
            Type = NodeType.TARGET_PLAYER_DISTANCE;

            

            NodeName.Content = "Player distance";

            AddParam(ParamId.PARAM_1, "Max dist");
        }

        public override Node Clone()
        {
            return new PlayerDistance();
        }
    }
}
