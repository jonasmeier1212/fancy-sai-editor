using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NodeAI.Nodes.ActionNodes
{
    [Node(MenuName = "Force despawn", Type = NodeType.ACTION_FORCE_DESPAWN, AllowedTypes = new NodeType[] { NodeType.EVENT, NodeType.TARGET })]
    class ForceDepspawn : ActionNode
    {
        public ForceDepspawn()
        {
            Type = NodeType.ACTION_FORCE_DESPAWN;

            //Update text
            NodeName.Content = "Force despawn";

            AddParam(ParamId.PARAM_1, "Despawn timer:");
            AddParam(ParamId.PARAM_2, "Respawn timer:");
        }

        public override Node Clone()
        {
            return new ForceDepspawn();
        }
    }
}
