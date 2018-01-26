using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_REACHED_HOME
    /// 
    /// </summary>
    [Node(MenuName = "Reached home", Type = NodeType.EVENT_RESPAWN, AllowedTypes = new NodeType[] { NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class ReachedHome : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public ReachedHome()
        {
            Type = NodeType.EVENT_RESPAWN;

            EventId = "21";

            //Update text
            NodeName.Content = "Reached home";
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new ReachedHome();
        }
    }
}
