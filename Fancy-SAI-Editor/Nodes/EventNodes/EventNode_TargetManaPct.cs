using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_TARGET_MANA_PCT
    /// 
    /// </summary>
    [Node(MenuName = "Target mana percentage", Type = NodeType.EVENT_TARGET_MANA_PCT, AllowedTypes = new NodeType[] { NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class TargetManaPct : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public TargetManaPct()
        {
            Type = NodeType.EVENT_TARGET_MANA_PCT;

            EventId = "18";

            //Update text
            NodeName.Content = "Target mana percentage";
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
            return new TargetManaPct();
        }
    }
}
