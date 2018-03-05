using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancySaiEditor.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_EVADE
    ///
    /// </summary>
    [Node(MenuName = "Evade", Type = NodeType.EVENT_AGGRO, AllowedTypes = new NodeType[] { NodeType.AI_OWNER, NodeType.ACTION })]
    public class Evade : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public Evade()
        {
            Type = NodeType.EVENT_MANA_PTC;

            

            //Update text
            NodeName.Content = "Evade";
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new Evade();
        }
    }
}
