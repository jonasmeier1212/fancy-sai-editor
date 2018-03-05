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
    [Node(MenuName = "Distance gameobject", Type = NodeType.EVENT_DISTANCE_GAMEOBJECT, AllowedTypes = new NodeType[] { NodeType.AI_OWNER, NodeType.ACTION })]
    public class DistanceGameObject : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public DistanceGameObject()
        {
            Type = NodeType.EVENT_DISTANCE_GAMEOBJECT;

            

            //Update text
            NodeName.Content = "Distance gameobject";
            AddParam(ParamId.PARAM_1, "NPC Guid:");
            AddParam(ParamId.PARAM_2, "NPC Entry:");
            AddParam(ParamId.PARAM_3, "Distance:");
            AddParam(ParamId.PARAM_4, "Repeat interval:");
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new DistanceGameObject();
        }
    }
}
