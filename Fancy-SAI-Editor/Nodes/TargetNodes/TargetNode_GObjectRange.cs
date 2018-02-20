using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.TargetNodes
{
    [Node(MenuName = "Gameobject Range", Type = NodeType.TARGET_GAMEOBJECT_RANGE, AllowedTypes = new NodeType[] { NodeType.ACTION })]
    public class GameobjectRange : TargetNode
    {
        public GameobjectRange()
        {
            Type = NodeType.TARGET_GAMEOBJECT_RANGE;

            

            NodeName.Content = "Gameobject Range";
        }

        public override Node Clone()
        {
            return new GameobjectRange();
        }
    }
}
