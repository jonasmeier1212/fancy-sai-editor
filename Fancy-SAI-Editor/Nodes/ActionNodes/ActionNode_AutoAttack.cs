using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NodeAI.Nodes.ActionNodes
{
    [Node(MenuName = "AutoAttack", Type = NodeType.ACTION_AUTO_ATTACK, AllowedTypes = new NodeType[] { NodeType.EVENT, NodeType.TARGET })]
    class AutoAttack : ActionNode
    {
        public AutoAttack()
        {
            Type = NodeType.ACTION_AUTO_ATTACK;

            //Update text
            NodeName.Content = "Auto attack";
            AddParam<AutoAttackTypes>(ParamId.PARAM_1, "");
        }

        enum AutoAttackTypes
        {
            STOP_ATTACK,
            ATTACK,
        }

        public override Node Clone()
        {
            return new AutoAttack();
        }
    }
}
