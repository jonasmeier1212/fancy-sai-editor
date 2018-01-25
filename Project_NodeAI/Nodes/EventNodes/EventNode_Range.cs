using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_RANGE
    ///
    /// </summary>
    [Node(MenuName = "Range", Type = NodeType.EVENT_RANGE, AllowedTypes = new NodeType[] { NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class Range : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public Range()
        {
            Type = NodeType.EVENT_RANGE;

            EventId = "9";

            //Update text
            NodeName.Content = "Range";

            AddParam(ParamId.PARAM_1, "Min Dist");
            AddParam(ParamId.PARAM_2, "Max Dist");
            AddParam(ParamId.PARAM_3, "Repeat min");
            AddParam(ParamId.PARAM_4, "Repeat max");
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new Range();
        }
    }
}
