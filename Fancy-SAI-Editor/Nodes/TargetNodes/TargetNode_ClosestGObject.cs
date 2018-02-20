using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.TargetNodes
{
    [Node(MenuName = "Closest Gameobject", Type = NodeType.TARGET_CLOSEST_GAMEOBJECT, AllowedTypes = new NodeType[] { NodeType.GENERAL_GAMEOBJECT, NodeType.ACTION })]
    public class ClosestGameobject : TargetNode
    {
        public ClosestGameobject()
        {
            Type = NodeType.TARGET_SELF;

            

            NodeName.Content = "Closest Gameobject";

            AddParam<GeneralNodes.GObject>(ParamId.PARAM_1, NodeType.GENERAL_GAMEOBJECT, "Gameobject");
            AddParam(ParamId.PARAM_2, "Max Dist:");
        }

        public override Node Clone()
        {
            return new ClosestGameobject();
        }
    }
}
