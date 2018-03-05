using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancySaiEditor.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_ACTION_DONE
    ///
    /// </summary>
    [Node(MenuName = "Action done", Type = NodeType.EVENT_ACTION_DONE, AllowedTypes = new NodeType[] { NodeType.AI_OWNER, NodeType.ACTION })]
    public class ActionDone : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public ActionDone()
        {
            Type = NodeType.EVENT_ACTION_DONE;

            

            //Update text
            NodeName.Content = "Action done";

            AddParam(ParamId.PARAM_1, "EventID:");
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new ActionDone();
        }
    }
}
