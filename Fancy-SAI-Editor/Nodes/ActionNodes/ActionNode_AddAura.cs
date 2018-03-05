using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancySaiEditor.Nodes.ActionNodes
{
    [Node(MenuName = "Add aura", Type = NodeType.ACTION_ADD_AURA, AllowedTypes = new NodeType[] { NodeType.PARAM_SPELL, NodeType.EVENT, NodeType.TARGET })]
    class AddAura : ActionNode
    {
        public AddAura()
        {
            Type = NodeType.ACTION_CLOSE_GOSSIP;

            

            //Update text
            NodeName.Content = "Add aura";

            AddParam<ParamNodes.Spell>(ParamId.PARAM_1, NodeType.PARAM_SPELL, "Aura");
        }

        public override Node Clone()
        {
            return new AddAura();
        }
    }
}
