using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_HEALTH_PCT
    ///
    /// </summary>
    [Node(MenuName = "Health Percentage", Type = NodeType.EVENT_HEALTH_PTC, AllowedTypes = new NodeType[] { NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class HealthPct : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public HealthPct()
        {
            Type = NodeType.EVENT_HEALTH_PTC;

            EventId = "2";

            //Update text
            NodeName.Content = "Health Percentage";

            AddParam(ParamId.PARAM_1, "HP min %");
            AddParam(ParamId.PARAM_2, "HP max %");
            AddParam(ParamId.PARAM_3, "Repeat min");
            AddParam(ParamId.PARAM_4, "Repeat max");
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new HealthPct();
        }
    }
}
