using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FancySaiEditor.Nodes.ActionNodes
{
    [Node(MenuName = "Set Faction", Type = NodeType.ACTION_SET_FACTION, AllowedTypes = new NodeType[] { NodeType.PARAM_FACTION, NodeType.EVENT, NodeType.TARGET })]
    class SetFaction : ActionNode
    {
        public SetFaction()
        {
            Type = NodeType.ACTION_SET_FACTION;

            

            //Update text
            NodeName.Content = "Set Faction";

            AddParam<ParamNodes.Faction>(ParamId.PARAM_1, NodeType.PARAM_FACTION, "Faction");
        }

        public override Node Clone()
        {
            return new SetFaction();
        }
    }
}
