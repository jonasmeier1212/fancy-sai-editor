using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancySaiEditor.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_AGGRO
    ///
    /// </summary>
    [Node(MenuName = "Aggro", Type = NodeType.EVENT_AGGRO, AllowedTypes = new NodeType[] { NodeType.AI_OWNER, NodeType.ACTION })]
    public class Aggro : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public Aggro()
        {
            Type = NodeType.EVENT_AGGRO;

            

            //Update text
            NodeName.Content = "Aggro";
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new Aggro();
        }
    }
}
