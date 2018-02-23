using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancySaiEditor.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_RECEIVE_HEAL
    ///
    /// </summary>
    [Node(MenuName = "Receive heal", Type = NodeType.EVENT_RECEIVE_HEAL, AllowedTypes = new NodeType[] { NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class ReceiveHeal : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public ReceiveHeal()
        {
            Type = NodeType.EVENT_RECEIVE_HEAL;

            

            //Update text
            NodeName.Content = "Receive heal";

            AddParam(ParamId.PARAM_1, "Min heal:");
            AddParam(ParamId.PARAM_2, "Max heal:");
            AddParam(ParamId.PARAM_3, "Cooldown min:");
            AddParam(ParamId.PARAM_4, "Cooldown max:");
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new ReceiveHeal();
        }
    }
}
