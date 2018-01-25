using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_LOS_OOC
    /// SMART_EVENT_LOS_IC
    /// </summary>
    [Node(MenuName = "Line of sight", Type = NodeType.EVENT_LOS, AllowedTypes = new NodeType[] { NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class LOS : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public LOS()
        {
            Type = NodeType.EVENT_LOS;

            //Update text
            NodeName.Content = "Line of sight";

            AddParam<LOSTypes>(ParamId.PARAM_SPECIFIC_TYPE, "LOS Type");
            AddParam<HostileTypes>(ParamId.PARAM_1, "");
            AddParam(ParamId.PARAM_2, "Max Range");
            AddParam(ParamId.PARAM_3, "Cooldown min");
            AddParam(ParamId.PARAM_4, "Cooldown max");
        }

        enum LOSTypes
        {
            OUT_OF_COMBAT = 10,
            IN_COMBAT = 26,
        }

        enum HostileTypes
        {
            HOSTILE,
            NO_HOSTILE,
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new LOS();
        }
    }
}
