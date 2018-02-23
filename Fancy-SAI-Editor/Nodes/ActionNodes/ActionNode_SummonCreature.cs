using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FancySaiEditor.Nodes.ActionNodes
{
    [Node(MenuName = "Summon creature", Type = NodeType.ACTION_SUMMON_CREATURE, AllowedTypes = new NodeType[] { NodeType.GENERAL_NPC, NodeType.EVENT, NodeType.TARGET })]
    class SummonCreature : ActionNode
    {
        public SummonCreature()
        {
            Type = NodeType.ACTION_SUMMON_CREATURE;

            

            //Update text
            NodeName.Content = "Summon creature";

            AddParam<GeneralNodes.Npc>(ParamId.PARAM_1, NodeType.GENERAL_NPC, "NPC");
        }

        public override Node Clone()
        {
            return new SummonCreature();
        }
    }
}
