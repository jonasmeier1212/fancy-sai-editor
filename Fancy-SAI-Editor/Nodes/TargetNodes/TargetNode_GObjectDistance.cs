using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.TargetNodes
{
    [Node(MenuName = "Gameobject Distance", Type = NodeType.TARGET_GAMEOBJECT_DISTANCE, AllowedTypes = new NodeType[] { NodeType.ACTION })]
    public class GObjectDistance : TargetNode
    {
        public GObjectDistance()
        {
            Type = NodeType.TARGET_GAMEOBJECT_DISTANCE;

            TargetId = "15";

            NodeName.Content = "Gameobject Distance";
        }

        public override Node Clone()
        {
            return new GObjectDistance();
        }
    }
}
