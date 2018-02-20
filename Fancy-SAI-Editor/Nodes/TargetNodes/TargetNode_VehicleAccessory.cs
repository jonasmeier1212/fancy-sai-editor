using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.TargetNodes
{
    [Node(MenuName = "Vehicle Accessory", Type = NodeType.TARGET_VEHICLE_ACCESSORY, AllowedTypes = new NodeType[] { NodeType.ACTION })]
    public class VehicleAccessory : TargetNode
    {
        public VehicleAccessory()
        {
            Type = NodeType.TARGET_VEHICLE_ACCESSORY;

            

            NodeName.Content = "Vehicle Accessory";

            AddParam(ParamId.PARAM_1, "Seat Id");
        }

        public override Node Clone()
        {
            return new VehicleAccessory();
        }
    }
}
