using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancySaiEditor.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_MOVEMENTINFORM
    ///
    /// </summary>
    [Node(MenuName = "Movement inform", Type = NodeType.EVENT_MOVEMENTINFORM, AllowedTypes = new NodeType[] { NodeType.AI_OWNER, NodeType.ACTION })]
    public class MovementInform : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public MovementInform()
        {
            Type = NodeType.EVENT_MOVEMENTINFORM;

            

            //Update text
            NodeName.Content = "Movement inform";

            AddParam(ParamId.PARAM_1, "Movement type:"); // TODO: Replace with enum
            AddParam(ParamId.PARAM_2, "Point ID:");
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new MovementInform();
        }
    }
}
