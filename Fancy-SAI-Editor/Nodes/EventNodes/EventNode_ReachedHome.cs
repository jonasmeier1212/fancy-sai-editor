using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancySaiEditor.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_REACHED_HOME
    /// 
    /// </summary>
    [Node(MenuName = "Reached home", Type = NodeType.EVENT_REACHED_HOME, AllowedTypes = new NodeType[] { NodeType.AI_OWNER, NodeType.ACTION })]
    public class ReachedHome : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public ReachedHome()
        {
            Type = NodeType.EVENT_REACHED_HOME;

            

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
