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
    [Node(MenuName = "GameObject state changed", Type = NodeType.EVENT_GO_STATE_CHANGED, AllowedTypes = new NodeType[] { NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class GoStateChanged : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public GoStateChanged()
        {
            Type = NodeType.EVENT_GO_STATE_CHANGED;

            EventId = "70";

            //Update text
            NodeName.Content = "GameObject state changed";

            AddParam<GameObjectState>(ParamId.PARAM_1, "State:");
        }

        enum GameObjectState
        {
            ACTIVE,
            READY,
            ACTIVE_ALTERNATIVE,
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new GoStateChanged();
        }
    }
}
