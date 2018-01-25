using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NodeAI.Nodes.ActionNodes
{
    [Node(MenuName = "Cast", Type = NodeType.ACTION_CAST, AllowedTypes = new NodeType[] { NodeType.GENERAL_SPELL, NodeType.EVENT, NodeType.TARGET })]
    class Cast : ActionNode
    {
        public Cast()
        {
            Type = NodeType.ACTION_CAST;

            //Update text
            NodeName.Content = "Cast";

            AddParam<CastFlags>(ParamId.PARAM_1, "Flags");
            AddParam<GeneralNodes.Spell>(ParamId.PARAM_2, NodeType.GENERAL_SPELL, "Spell");
        }

        public override Node Clone()
        {
            return new Cast();
        }
    }
    [Flags]
    enum CastFlags
    {
        SMARTCAST_INTERRUPT_PREVIOUS    = 0x01,
        SMARTCAST_TRIGGERED             = 0x02,
        CAST_AURA_NOT_PRESENT           = 0x20,
        SMARTCAST_COMBAT_MOVE           = 0x40,
    }
}
