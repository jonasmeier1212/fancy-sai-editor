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

namespace FancySaiEditor.Nodes.GeneralNodes
{
    [Node(MenuName = "Faction", Type = NodeType.GENERAL_FACTION)]
    class Faction : GeneralNode
    {
        public Faction()
        {
            NodeData = new FactionData();

            Type = NodeType.GENERAL_FACTION;

            //Update text
            NodeName.Content = "Faction";

            AddDatabaseSelectionFrame(new DataSelectionPossibility("Name:", "name"));
        }

        public override Node Clone()
        {
            return new Faction();
        }

        public override string GetParamValue()
        {
            try
            {
                return NodeData.Rows[0].ItemArray[0].ToString();
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Faction node has no faction selected!");
                throw new ArgumentException();
            }
            catch (FormatException)
            {
                MessageBox.Show("ID is not a number!");
                throw new ArgumentException();
            }
        }

        public async override void SetParamValue(string value)
        {
            await Database.SelectSqliteData("ID", value, NodeData);
            SelectData(NodeData);
        }
    }
}
