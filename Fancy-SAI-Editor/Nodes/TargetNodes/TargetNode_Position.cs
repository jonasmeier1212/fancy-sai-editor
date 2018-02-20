using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.TargetNodes
{
    [Node(MenuName = "Position", Type = NodeType.TARGET_POSITION, AllowedTypes = new NodeType[] { NodeType.ACTION })]
    public class Position : TargetNode
    {
        public Position()
        {
            Type = NodeType.TARGET_POSITION;

            

            NodeName.Content = "Position";

            AddParam(ParamId.PARAM_4, "X:");
            AddParam(ParamId.PARAM_5, "Y:");
            AddParam(ParamId.PARAM_6, "Z:");
            AddParam(ParamId.PARAM_7, "O:");
        }

        public override Node Clone()
        {
            return new Position();
        }
    }
}
