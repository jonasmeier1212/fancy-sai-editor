using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancySaiEditor.Nodes.TargetNodes
{
    [Node(MenuName = "Action invoker vehicle", Type = NodeType.TARGET_ACTION_INVOKER_VEHICLE, AllowedTypes = new NodeType[] { NodeType.ACTION })]
    public class ActionInvokerVehicle : TargetNode
    {
        public ActionInvokerVehicle()
        {
            Type = NodeType.TARGET_ACTION_INVOKER_VEHICLE;

            

            NodeName.Content = "Action invoker vehicle";
        }

        public override Node Clone()
        {
            return new ActionInvokerVehicle();
        }
    }
}
