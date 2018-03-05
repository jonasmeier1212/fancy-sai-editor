using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FancySaiEditor.Nodes.ActionNodes
{
    [Node(MenuName = "Summon creature", Type = NodeType.ACTION_SUMMON_CREATURE, AllowedTypes = new NodeType[] { NodeType.PARAM_NPC, NodeType.EVENT, NodeType.TARGET })]
    class SummonCreature : ActionNode
    {
        public SummonCreature()
        {
            Type = NodeType.ACTION_SUMMON_CREATURE;

            

            //Update text
            NodeName.Content = "Summon creature";

            AddParam<ParamNodes.Npc>(ParamId.PARAM_1, NodeType.PARAM_NPC, "NPC");
        }

        public override Node Clone()
        {
            return new SummonCreature();
        }
    }
}
