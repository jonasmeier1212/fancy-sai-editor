using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancySaiEditor.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_UPDATE_IC
    /// SMART_EVENT_UPDATE_OOC
    /// </summary>
    [Node(MenuName = "Update in combat", Type = NodeType.EVENT_UPDATE_IC, AllowedTypes = new NodeType[] { NodeType.AI_OWNER, NodeType.ACTION })]
    public class UpdateIC : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public UpdateIC()
        {
            Type = NodeType.EVENT_UPDATE_IC;

            //Update text
            NodeName.Content = "Update in combat";

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
            return new UpdateIC();
        }
    }
}
