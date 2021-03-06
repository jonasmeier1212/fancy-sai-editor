﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FancySaiEditor.Nodes.ParamNodes
{
    [Node(MenuName = "Npc Model", Type = NodeType.PARAM_NPC_MODEL)]
    class NpcModel : ParamNode
    {
        public NpcModel()
        {
            NodeData = new NodeData();
            //NodeData = new NpcModelData();

            Type = NodeType.PARAM_NPC_MODEL;

            NodeName.Content = "Npc model";
        }

        public override Node Clone()
        {
            return new NpcModel();
        }

        public override string GetParamValue()
        {
            try
            {
                return NodeData.Rows[0].ItemArray[0].ToString();
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Npc model node has no faction selected!");
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
