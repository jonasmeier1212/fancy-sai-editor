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
    [Node(MenuName = "AI init", Type = NodeType.EVENT_AI_INIT, AllowedTypes = new NodeType[] { NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class AIInit : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public AIInit()
        {
            Type = NodeType.EVENT_AI_INIT;

            

            //Update text
            NodeName.Content = "AI init";
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new AIInit();
        }
    }
}
