using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NodeAI.Nodes.ActionNodes
{
    [Node(MenuName = "Set Faction", Type = NodeType.ACTION_SET_FACTION, AllowedTypes = new NodeType[] { NodeType.GENERAL_FACTION, NodeType.EVENT, NodeType.TARGET })]
    class SetFaction : ActionNode
    {
        public SetFaction()
        {
            Type = NodeType.ACTION_SET_FACTION;

            ActionId = "2";

            //Update text
            NodeName.Content = "Set Faction";

            AddParam<GeneralNodes.Faction>(ParamId.PARAM_1, NodeType.GENERAL_FACTION, "Faction");
        }

        public override Node Clone()
        {
            return new SetFaction();
        }
    }
}
