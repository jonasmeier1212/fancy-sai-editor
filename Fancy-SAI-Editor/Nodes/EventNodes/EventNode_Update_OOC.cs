using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_UPDATE_IC
    /// SMART_EVENT_UPDATE_OOC
    /// </summary>
    [Node(MenuName = "Update Out of Combat", Type = NodeType.EVENT_UPDATE_OOC, AllowedTypes = new NodeType[] { NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class UpdateOOC : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public UpdateOOC()
        {
            Type = NodeType.EVENT_UPDATE_OOC;

            //Update text
            NodeName.Content = "Update Out of Combat";

            AddParam(ParamId.PARAM_1, "Initial min");
            AddParam(ParamId.PARAM_2, "Initial max");
            AddParam(ParamId.PARAM_3, "Repeat min");
            AddParam(ParamId.PARAM_4, "Repeat max");
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new UpdateOOC();
        }
    }
}
