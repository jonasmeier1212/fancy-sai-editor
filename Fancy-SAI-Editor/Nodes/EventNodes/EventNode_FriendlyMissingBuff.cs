using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancySaiEditor.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_FRIENDLY_MISSING_BUFF
    /// 
    /// </summary>
    [Node(MenuName = "Friendly missing buff", Type = NodeType.EVENT_FRIENDLY_MISSING_BUFF, AllowedTypes = new NodeType[] { NodeType.AI_OWNER, NodeType.ACTION })]
    public class FriendlyMissingBuff : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public FriendlyMissingBuff()
        {
            Type = NodeType.EVENT_FRIENDLY_MISSING_BUFF;

            

            //Update text
            NodeName.Content = "Friendly missing buff";

            AddParam<ParamNodes.Spell>(ParamId.PARAM_1, NodeType.PARAM_SPELL, "Buff");
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
            return new FriendlyMissingBuff();
        }
    }
}
