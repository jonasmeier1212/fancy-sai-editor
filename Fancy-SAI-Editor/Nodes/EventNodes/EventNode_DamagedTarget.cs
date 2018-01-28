using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_AGGRO
    ///
    /// </summary>
    [Node(MenuName = "Damaged target", Type = NodeType.EVENT_DAMAGED_TARGET, AllowedTypes = new NodeType[] { NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class DamagedTarget : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public DamagedTarget()
        {
            Type = NodeType.EVENT_DAMAGED_TARGET;

            EventId = "33";

            //Update text
            NodeName.Content = "Damaged target";

            AddParam(ParamId.PARAM_1, "Min damage:");
            AddParam(ParamId.PARAM_2, "Max damage:");
            AddParam(ParamId.PARAM_3, "Repeat min:");
            AddParam(ParamId.PARAM_4, "Repeat max:");
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new DamagedTarget();
        }
    }
}
