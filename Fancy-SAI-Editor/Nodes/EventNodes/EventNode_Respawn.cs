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
    [Node(MenuName = "Respawn", Type = NodeType.EVENT_RESPAWN, AllowedTypes = new NodeType[] { NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class Respawn : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public Respawn()
        {
            Type = NodeType.EVENT_RESPAWN;

            

            //Update text
            NodeName.Content = "Respawn";

            AddParam(ParamId.PARAM_1, "Type");
            AddParam(ParamId.PARAM_2, "MapId");
            AddParam(ParamId.PARAM_3, "Zone Id");
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new Respawn();
        }
    }
}
