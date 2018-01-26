using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.TargetNodes
{
    [Node(MenuName = "Action Invoker", Type = NodeType.TARGET_ACTION_INVOKER, AllowedTypes = new NodeType[] { NodeType.ACTION })]
    public class ActionInvoker : TargetNode
    {
        public ActionInvoker()
        {
            Type = NodeType.TARGET_ACTION_INVOKER;

            TargetId = "7";

            NodeName.Content = "Action Invoker";
        }

        public override Node Clone()
        {
            return new ActionInvoker();
        }
    }
}
