using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancySaiEditor.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_LOS_OOC
    /// SMART_EVENT_LOS_IC
    /// </summary>
    [Node(MenuName = "In combat line of sight", Type = NodeType.EVENT_IC_LOS, AllowedTypes = new NodeType[] { NodeType.AI_OWNER, NodeType.ACTION })]
    public class ICLOS : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public ICLOS()
        {
            Type = NodeType.EVENT_IC_LOS;

            //Update text
            NodeName.Content = "In combat line of sight";

            AddParam<HostileTypes>(ParamId.PARAM_1, "");
            AddParam(ParamId.PARAM_2, "Max Range");
            AddParam(ParamId.PARAM_3, "Cooldown min");
            AddParam(ParamId.PARAM_4, "Cooldown max");
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
            return new ICLOS();
        }
    }
}
