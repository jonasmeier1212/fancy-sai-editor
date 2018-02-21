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
    /// <summary>
    /// Node for selecting a NPC.
    /// 
    /// Output:
    ///     - NPC for event node for example.
    /// </summary>
    [Node(MenuName = "NPC", Type = NodeType.GENERAL_NPC)]
    public class Npc : GeneralNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public Npc()
        {
            NodeData = new NpcData();

            Type = NodeType.GENERAL_NPC;

            //Update text
            NodeName.Content = "NPC";

            AddDatabaseSelectionFrame(new DataSelectionPossibility("Name", "name"));
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new Npc();
        }

        /// <summary>
        /// Returns entry of the NPC selected in this node.
        /// </summary>
        /// <returns>Return entry. If there is no NPC selected, it shows a warning.</returns>
        public override string GetParamValue()
        {
            try
            {
                return NodeData.Rows[0].ItemArray[0].ToString();
            }
            catch (IndexOutOfRangeException)
            {
                throw new ExportException("Not all NPC Nodes has a selected NPC!");
            }
        }

        public bool IsSAIOwner()
        {
            //TODO: This doesn't work with every event node! Some event nodes have more than one connector for npcs

            bool correct = true;
            var dirConEventNodes = GetDirectlyConnectedNodes(NodeType.EVENT, NodeConnectorType.OUTPUT);
            
            if (dirConEventNodes.Count == 0)
                return false;

            return correct;
        }

        public override void SetParamValue(int value)
        {
            throw new NotImplementedException();
        }
    }
}
