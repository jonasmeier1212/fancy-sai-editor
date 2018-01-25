using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

namespace NodeAI.Nodes.GeneralNodes
{
    [Node( MenuName = "Text", Type = NodeType.GENERAL_TEXT)]
    class Text : GeneralNode
    {
        public Text()
        {
            NodeData = new TextData();
            Type = NodeType.GENERAL_TEXT;

            //Update text
            NodeName.Content = "Text";

            AddDatabaseSelectionFrame(new DataSelectionPossibility("Text", "text") ,new AdditionalTab(new CreatureTextCreation(), "Create"));
            
        }

        public override Node Clone()
        {
            return new Text();
        }

        public override string GetParamValue()
        {
            try
            {
                return NodeData.Rows[0].ItemArray[0].ToString();
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Not all Text Nodes has selected text!");
                throw new ArgumentException();
            }
            catch (FormatException)
            {
                MessageBox.Show("GroupID is not a number!");
                throw new ArgumentException();
            }
        }
    }
}
