using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_RESPAWN
    /// 
    /// </summary>
    [Node(MenuName = "Passenger", Type = NodeType.EVENT_PASSENGER, AllowedTypes = new NodeType[] { NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class Passenger : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public Passenger()
        {
            Type = NodeType.EVENT_PASSENGER;

            EventId = "11";

            //Update text
            NodeName.Content = "Passenger";

            AddParam<PassengerEventTypes>(ParamId.PARAM_SPECIFIC_TYPE, "Type:");
            AddParam(ParamId.PARAM_1, "Cooldown min:");
            AddParam(ParamId.PARAM_2, "Cooldown max:");
        }

        enum PassengerEventTypes
        {
            BOARDED = 27,
            REMOVED = 28,
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new Passenger();
        }
    }
}
