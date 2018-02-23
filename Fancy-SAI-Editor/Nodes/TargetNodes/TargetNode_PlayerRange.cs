using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancySaiEditor.Nodes.TargetNodes
{
    [Node(MenuName = "Player range", Type = NodeType.TARGET_PLAYER_RANGE, AllowedTypes = new NodeType[] { NodeType.ACTION })]
    public class PlayerRange : TargetNode
    {
        public PlayerRange()
        {
            Type = NodeType.TARGET_PLAYER_RANGE;

            

            NodeName.Content = "Player range";

            AddParam(ParamId.PARAM_1, "Min Dist:");
            AddParam(ParamId.PARAM_2, "Max Dist:");
        }

        public override Node Clone()
        {
            return new PlayerRange();
        }
    }
}
