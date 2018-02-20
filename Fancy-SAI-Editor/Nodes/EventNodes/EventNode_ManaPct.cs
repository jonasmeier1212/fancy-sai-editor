using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_MANA_PCT
    ///
    /// </summary>
    [Node(MenuName = "Mana Percentage", Type = NodeType.EVENT_MANA_PTC, AllowedTypes = new NodeType[] { NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class ManaPct : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public ManaPct()
        {
            Type = NodeType.EVENT_MANA_PTC;

            

            //Update text
            NodeName.Content = "Mana Percentage";

            AddParam(ParamId.PARAM_1, "Mana min %");
            AddParam(ParamId.PARAM_2, "Mana max %");
            AddParam(ParamId.PARAM_3, "Repeat min");
            AddParam(ParamId.PARAM_4, "Repeat max");
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new ManaPct();
        }
    }
}
