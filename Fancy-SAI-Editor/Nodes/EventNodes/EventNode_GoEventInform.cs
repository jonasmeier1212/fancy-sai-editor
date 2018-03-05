using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancySaiEditor.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_GO_EVENT_INFORM
    ///
    /// </summary>
    [Node(MenuName = "GameObject event inform", Type = NodeType.EVENT_GO_EVENT_INFORM, AllowedTypes = new NodeType[] { NodeType.AI_OWNER, NodeType.ACTION })]
    public class GoEventInform : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public GoEventInform()
        {
            Type = NodeType.EVENT_GO_EVENT_INFORM;

            

            //Update text
            NodeName.Content = "GameObject event inform";

            AddParam(ParamId.PARAM_1, "EventID:");
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new GoEventInform();
        }
    }
}
