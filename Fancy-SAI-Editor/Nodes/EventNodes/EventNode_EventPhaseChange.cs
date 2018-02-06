using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_PHASE_CHANGED
    ///
    /// </summary>
    [Node(MenuName = "Event phase changed", Type = NodeType.EVENT_EVENT_PHASE_CHANGED, AllowedTypes = new NodeType[] { NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class EventPhaseChanged : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public EventPhaseChanged()
        {
            Type = NodeType.EVENT_EVENT_PHASE_CHANGED;

            EventId = "66";

            //Update text
            NodeName.Content = "Event phase changed";

            AddParam(ParamId.PARAM_1, "Event phase mask:");
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new EventPhaseChanged();
        }
    }
}
