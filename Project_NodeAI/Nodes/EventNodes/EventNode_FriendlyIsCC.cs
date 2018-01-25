using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_FRIENDLY_IS_CC
    /// 
    /// </summary>
    [Node(MenuName = "Friendly is CC", Type = NodeType.EVENT_FRIENDLY_IS_CC, AllowedTypes = new NodeType[] { NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class FriendlyIsCC : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public FriendlyIsCC()
        {
            Type = NodeType.EVENT_FRIENDLY_IS_CC;

            EventId = "15";

            //Update text
            NodeName.Content = "Friendly is CC";

            AddParam(ParamId.PARAM_1, "Radius");
            AddParam(ParamId.PARAM_2, "Repeat min");
            AddParam(ParamId.PARAM_3, "Repeat max");
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new FriendlyIsCC();
        }
    }
}
