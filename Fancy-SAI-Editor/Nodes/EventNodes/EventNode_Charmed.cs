using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_RESPAWN
    /// 
    /// </summary>
    [Node(MenuName = "Charmed", Type = NodeType.EVENT_CHARMED, AllowedTypes = new NodeType[] { NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class Charmed : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public Charmed()
        {
            Type = NodeType.EVENT_CHARMED;

            

            //Update text
            NodeName.Content = "Charmed";

            AddParam<CharmType>(ParamId.PARAM_1, "");
        }

        enum CharmType
        {
            ON_CHARM_APPLY,
            ON_CHARM_REMOVE,
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new Charmed();
        }
    }
}
