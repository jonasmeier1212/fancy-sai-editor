using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FancySaiEditor.Nodes.ActionNodes
{
    [Node(MenuName = "AllowCombatMovement", Type = NodeType.ACTION_ALLOW_COMBAT_MOVEMENT, AllowedTypes = new NodeType[] { NodeType.EVENT, NodeType.TARGET })]
    class AllowCombatMovement : ActionNode
    {
        public AllowCombatMovement()
        {
            Type = NodeType.ACTION_ALLOW_COMBAT_MOVEMENT;

            //Update text
            NodeName.Content = "Allow Combat Movement";

            AddParam<AllowCombatMovementTypes>(ParamId.PARAM_1, "");
        }

        enum AllowCombatMovementTypes
        {
            ALLOW,
            PROHIBIT,
        }

        public override Node Clone()
        {
            return new AllowCombatMovement();
        }
    }
}
