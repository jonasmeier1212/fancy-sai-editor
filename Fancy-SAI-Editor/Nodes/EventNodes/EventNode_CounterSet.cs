using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_COUNTER_SET
    ///
    /// </summary>
    [Node(MenuName = "Counter set", Type = NodeType.EVENT_COUNTER_SET, AllowedTypes = new NodeType[] { NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class CounterSet : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public CounterSet()
        {
            Type = NodeType.EVENT_COUNTER_SET;

            

            //Update text
            NodeName.Content = "Counter set";

            AddParam(ParamId.PARAM_1, "Counter ID:");
            AddParam(ParamId.PARAM_2, "Value:");
            AddParam(ParamId.PARAM_3, "Cooldown min:");
            AddParam(ParamId.PARAM_4, "Cooldown max:");
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new CounterSet();
        }
    }
}
