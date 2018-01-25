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

namespace NodeAI.Nodes.GeneralNodes
{
    [Node(MenuName = "Quest", Type = NodeType.GENERAL_QUEST)]
    class Quest : GeneralNode
    {
        public Quest()
        {
            NodeData = new QuestData();

            Type = NodeType.GENERAL_QUEST;

            NodeName.Content = "Quest";

            AddDatabaseSelectionFrame(new DataSelectionPossibility("Quest title", "title"));
        }

        public override Node Clone()
        {
            return new Quest();
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
    }
}
