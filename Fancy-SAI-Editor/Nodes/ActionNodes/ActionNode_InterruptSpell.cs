using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FancySaiEditor.Nodes.ActionNodes
{
    [Node(MenuName = "Interrupt spell", Type = NodeType.ACTION_INTERRUPT_SPELL, AllowedTypes = new NodeType[] { NodeType.PARAM_SPELL, NodeType.EVENT, NodeType.TARGET })]
    class InterruptSpell : ActionNode
    {
        public InterruptSpell()
        {
            Type = NodeType.ACTION_INTERRUPT_SPELL;

            

            //Update text
            NodeName.Content = "Interrupt spell";

            AddParam<YesNo>(ParamId.PARAM_1, "With delay:");
            AddParam<ParamNodes.Spell>(ParamId.PARAM_2, NodeType.PARAM_SPELL, "Spell");
            AddParam<YesNo>(ParamId.PARAM_3, "Instant:");
        }

        public override Node Clone()
        {
            return new InterruptSpell();
        }
    }
}
