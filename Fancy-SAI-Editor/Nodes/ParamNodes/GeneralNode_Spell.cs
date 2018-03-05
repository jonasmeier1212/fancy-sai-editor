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
    [Node(MenuName = "Spell", Type = NodeType.PARAM_SPELL)]
    class Spell : ParamNode
    {
        public Spell()
        {
            NodeData = new SpellData();

            Type = NodeType.PARAM_SPELL;

            //Update text
            NodeName.Content = "Spell";

            AddDatabaseSelection();
        }

        public override Node Clone()
        {
            return new Spell();
        }

        public override string GetParamValue()
        {
            try
            {
                return NodeData.Rows[0].ItemArray[0].ToString();
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Spell node has no spell selected!");
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
            //await Database.SelectSqliteData("ID", value, NodeData);
            //SelectData(NodeData);
        }
    }
}
