using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancySaiEditor.Nodes.TargetNodes
{
    /// <summary>
    /// Base class for every target node
    /// </summary>
    public abstract class TargetNode : Node
    {
        public TargetNode()
        {
            AddInputConnector("Action", NodeType.ACTION);
        }

        public TargetData ExportData()
        {
            TargetData data = new TargetData()
            {
                targetType = GetRealId(Type, NodeType.TARGET).ToString(),
                targetParam1 = GetParam(ParamId.PARAM_1),
                targetParam2 = GetParam(ParamId.PARAM_2),
                targetParam3 = GetParam(ParamId.PARAM_3),
                target_x = GetParam(ParamId.PARAM_4),
                target_y = GetParam(ParamId.PARAM_5),
                target_z = GetParam(ParamId.PARAM_6),
                target_o = GetParam(ParamId.PARAM_7),
            };

            return data;
        }

        [Obsolete]
        public string TargetId { get; set; }
    }
}
