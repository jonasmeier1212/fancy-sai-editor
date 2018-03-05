using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Windows.Media;
using System.Windows.Input;

namespace FancySaiEditor.Nodes.ParamNodes
{
    [Node(MenuName = "Gossip menu", Type = NodeType.PARAM_GOSSIP_MENU)]
    class GossipMenu : ParamNode
    {
        public GossipMenu()
        {
            NodeData = new QuestData();

            Type = NodeType.PARAM_QUEST;

            NodeName.Content = "Gossip menu";

            AddDatabaseSelection();
        }

        public override Node Clone()
        {
            return new GossipMenu();
        }

        public override string GetParamValue()
        {
            try
            {
                return NodeData.Rows[0].ItemArray[0].ToString();
            }
            catch (IndexOutOfRangeException)
            {
                throw new ExportException("Not all Quest Nodes has selected quest!");
            }
        }

        public override void SetParamValue(string value)
        {
            throw new NotImplementedException();
        }
    }
}
