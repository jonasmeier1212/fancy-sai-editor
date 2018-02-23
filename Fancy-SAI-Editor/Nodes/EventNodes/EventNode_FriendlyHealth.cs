using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancySaiEditor.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_FRIENDLY_HEALTH
    /// 
    /// </summary>
    [Node(MenuName = "Friendly health", Type = NodeType.EVENT_FRIENDLY_HEALTH, AllowedTypes = new NodeType[] { NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class FriendlyHP : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public FriendlyHP()
        {
            Type = NodeType.EVENT_FRIENDLY_HEALTH;

            

            //Update text
            NodeName.Content = "Friendly health";

            AddParam(ParamId.PARAM_1, "HP Deficit");
            AddParam(ParamId.PARAM_2, "Radius");
            AddParam(ParamId.PARAM_3, "Repeat min");
            AddParam(ParamId.PARAM_4, "Repeat max");
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new FriendlyHP();
        }
    }
}
