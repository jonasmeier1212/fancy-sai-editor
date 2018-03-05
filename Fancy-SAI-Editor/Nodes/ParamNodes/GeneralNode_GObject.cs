using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FancySaiEditor.Nodes.ParamNodes
{
    [Node(MenuName = "Gameoject", Type = NodeType.PARAM_GAMEOBJECT)]
    class GObject : ParamNode
    {
        public GObject()
        {
            NodeData = new NodeData(); //TODO

            Type = NodeType.PARAM_GAMEOBJECT;

            //Update text
            NodeName.Content = "Gameobject";

            //TODO
        }

        public override Node Clone()
        {
            return new GObject();
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
