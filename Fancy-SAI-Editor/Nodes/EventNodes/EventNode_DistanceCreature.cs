using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_DISTANCE_CREATURE
    ///
    /// </summary>
    [Node(MenuName = "Distance creature", Type = NodeType.EVENT_DISTANCE_CREATURE, AllowedTypes = new NodeType[] { NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class DistanceCreature : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public DistanceCreature()
        {
            Type = NodeType.EVENT_DISTANCE_CREATURE;

            EventId = "75";

            //Update text
            NodeName.Content = "Distance creature";

            AddParam(ParamId.PARAM_1, "NPC Guid:");
            AddParam(ParamId.PARAM_2, "NPC Entry:");
            AddParam(ParamId.PARAM_3, "Distance:");
            AddParam(ParamId.PARAM_4, "Repeat interval:");
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new DistanceCreature();
        }
    }
}
