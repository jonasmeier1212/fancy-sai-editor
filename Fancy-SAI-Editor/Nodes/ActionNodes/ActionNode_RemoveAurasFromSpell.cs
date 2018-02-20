using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NodeAI.Nodes.ActionNodes
{
    [Node(MenuName = "Remove Auras From Spell", Type = NodeType.ACTION_REMOVEAURASFROMSPELL, AllowedTypes = new NodeType[] { NodeType.GENERAL_SPELL, NodeType.EVENT, NodeType.TARGET })]
    class RemoveAuraFromSpell : ActionNode
    {
        public RemoveAuraFromSpell()
        {
            Type = NodeType.ACTION_REMOVEAURASFROMSPELL;

            

            //Update text
            NodeName.Content = "Remove Auras From Spell";

            AddParam<GeneralNodes.Spell>(ParamId.PARAM_1, NodeType.GENERAL_SPELL, "Spell");
        }

        public override Node Clone()
        {
            return new RemoveAuraFromSpell();
        }
    }
}
