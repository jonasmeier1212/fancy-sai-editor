using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NodeAI.Nodes.GeneralNodes
{
    [Node(MenuName = "Sound", Type = NodeType.GENERAL_SOUND)]
    class Sound : GeneralNode
    {
        public Sound()
        {
            NodeData = new SoundData();

            Type = NodeType.GENERAL_SOUND;

            //Update text
            NodeName.Content = "Sound";

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
                MessageBox.Show("Sound node has no faction selected!");
                throw new ArgumentException();
            }
            catch (FormatException)
            {
                MessageBox.Show("ID is not a number!");
                throw new ArgumentException();
            }
        }
    }
}
