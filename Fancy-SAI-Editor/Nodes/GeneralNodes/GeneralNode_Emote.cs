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
    [Node(MenuName = "Emote", Type = NodeType.GENERAL_EMOTE)]
    class Emote : GeneralNode
    {
        public Emote()
        {
            NodeData = new EmoteData();

            Type = NodeType.GENERAL_EMOTE;

            //Update text
            NodeName.Content = "Emote";

            AddDatabaseSelectionFrame(new DataSelectionPossibility("Name:", "name"));
        }

        public override Node Clone()
        {
            return new Emote();
        }

        public override string GetParamValue()
        {
            try
            {
                return NodeData.Rows[0].ItemArray[0].ToString();
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Emote node has no Emote selected!");
                throw new ExportException();
            }
            catch (FormatException)
            {
                MessageBox.Show("ID is not a number!");
                throw new ExportException();
            }
        }

        public override void SetParamValue(string value)
        {
            throw new NotImplementedException();
        }
    }
}
