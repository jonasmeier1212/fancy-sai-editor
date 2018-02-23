using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancySaiEditor.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_RESPAWN
    /// 
    /// </summary>
    [Node(MenuName = "Passenger boarded", Type = NodeType.EVENT_PASSENGER_BOARDED, AllowedTypes = new NodeType[] { NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class PassengerBoarded : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public PassengerBoarded()
        {
            Type = NodeType.EVENT_PASSENGER_BOARDED;

            //Update text
            NodeName.Content = "Passenger boarded";

            AddParam(ParamId.PARAM_1, "Cooldown min:");
            AddParam(ParamId.PARAM_2, "Cooldown max:");
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new PassengerBoarded();
        }
    }
}
