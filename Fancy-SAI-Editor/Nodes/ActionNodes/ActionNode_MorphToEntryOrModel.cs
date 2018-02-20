using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NodeAI.Nodes.ActionNodes
{
    [Node(MenuName = "Morph to entry or model", Type = NodeType.ACTION_MORPH_TO_ENTRY_OR_MODEL, AllowedTypes = new NodeType[] { NodeType.GENERAL_NPC, NodeType.EVENT, NodeType.TARGET })]
    class MorphToEntryOrModel : ActionNode
    {
        public MorphToEntryOrModel()
        {
            Type = NodeType.ACTION_MORPH_TO_ENTRY_OR_MODEL;

            

            //Update text
            NodeName.Content = "Morph to entry or model";

            AddParam<GeneralNodes.Npc>(ParamId.PARAM_1, NodeType.GENERAL_NPC, "Creature Entry");
            AddParam<GeneralNodes.NpcModel>(ParamId.PARAM_2, NodeType.GENERAL_NPC_MODEL, "NPC Model");
        }

        public override Node Clone()
        {
            return new MorphToEntryOrModel();
        }
    }
}
