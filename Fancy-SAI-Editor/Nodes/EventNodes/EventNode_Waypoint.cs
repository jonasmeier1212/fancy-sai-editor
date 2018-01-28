using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_WAYPOINT_REACHED
    /// SMART_EVENT_WAYPOINT_START
    /// </summary>
    [Node(MenuName = "Waypoint", Type = NodeType.EVENT_WAYPOINT, AllowedTypes = new NodeType[] { NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class Waypoint : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public Waypoint()
        {
            Type = NodeType.EVENT_WAYPOINT;
            
            //Update text
            NodeName.Content = "Waypoint";
            AddParam<WaypointEventTypes>(ParamId.PARAM_SPECIFIC_TYPE, "Type");

            AddParam(ParamId.PARAM_1, "Point Id:");
            AddParam(ParamId.PARAM_2, "Path Id:"); // TODO: Replace with general node
        }

        enum WaypointEventTypes
        {
            WAYPOINT_REACHED = 40,
            WAYPOINT_START = 39,
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new Waypoint();
        }
    }
}
