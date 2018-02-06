using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_IS_BEHIND_TARGET
    ///
    /// </summary>
    [Node(MenuName = "Is behind target", Type = NodeType.EVENT_IS_BEHIND_TARGET, AllowedTypes = new NodeType[] { NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class IsBehindTarget : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public IsBehindTarget()
        {
            Type = NodeType.EVENT_IS_BEHIND_TARGET;

            EventId = "67";

            //Update text
            NodeName.Content = "Is behind target";

            AddParam(ParamId.PARAM_1, "Cooldown min:");
            AddParam(ParamId.PARAM_2, "Cooldown max:");
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new IsBehindTarget();
        }
    }
}
