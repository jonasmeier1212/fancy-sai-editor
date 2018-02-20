using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.TargetNodes
{
    [Node(MenuName = "Gameobject GUID", Type = NodeType.TARGET_GAMEOBJECT_GUID, AllowedTypes = new NodeType[] { NodeType.ACTION })]
    public class GObjectGuid : TargetNode
    {
        public GObjectGuid()
        {
            Type = NodeType.TARGET_GAMEOBJECT_GUID;

            

            NodeName.Content = "Gameobject GUID";
        }

        public override Node Clone()
        {
            return new GObjectGuid();
        }
    }
}
