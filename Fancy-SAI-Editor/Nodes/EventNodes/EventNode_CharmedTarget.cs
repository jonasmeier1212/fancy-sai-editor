using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancySaiEditor.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_RESPAWN
    /// 
    /// </summary>
    [Node(MenuName = "Charmed target", Type = NodeType.EVENT_CHARMED_TARGET, AllowedTypes = new NodeType[] { NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class CharmedTarget : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public CharmedTarget()
        {
            Type = NodeType.EVENT_CHARMED_TARGET;

            

            //Update text
            NodeName.Content = "Charmed target";
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new CharmedTarget();
        }
    }
}
