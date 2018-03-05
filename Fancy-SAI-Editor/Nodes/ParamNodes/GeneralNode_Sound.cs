using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FancySaiEditor.Nodes.ParamNodes
{
    [Node(MenuName = "Sound", Type = NodeType.PARAM_SOUND)]
    class Sound : ParamNode
    {
        public Sound()
        {
            NodeData = new SoundData();

            Type = NodeType.PARAM_SOUND;

            //Update text
            NodeName.Content = "Sound";

            AddDatabaseSelection();
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

        public override void SetParamValue(string value)
        {
            throw new NotImplementedException();
        }
    }
}
