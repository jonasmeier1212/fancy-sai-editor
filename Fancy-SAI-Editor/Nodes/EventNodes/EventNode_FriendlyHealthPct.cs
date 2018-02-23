using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancySaiEditor.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_FIRENDLY_HEALTH_PCT
    ///
    /// </summary>
    [Node(MenuName = "Friendly health pct", Type = NodeType.EVENT_FRIENDLY_HEALTH_PCT, AllowedTypes = new NodeType[] { NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class FriendlyHealthPct : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public FriendlyHealthPct()
        {
            Type = NodeType.EVENT_FRIENDLY_HEALTH_PCT;

            

            //Update text
            NodeName.Content = "Friendly health pct";

            AddParam(ParamId.PARAM_1, "Min HP Pct:");
            AddParam(ParamId.PARAM_2, "Max HP Pct:");
            AddParam(ParamId.PARAM_3, "Repeat min:");
            AddParam(ParamId.PARAM_4, "Repeat max:");
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new FriendlyHealthPct();
        }
    }
}
