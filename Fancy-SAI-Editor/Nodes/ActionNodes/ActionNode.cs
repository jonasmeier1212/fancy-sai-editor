using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancySaiEditor.Nodes.ActionNodes
{
    /// <summary>
    /// Base class for every action node
    /// </summary>
    public abstract class ActionNode : Node
    {
        public ActionNode()
        {
            AddInputConnector("Event", NodeType.EVENT);
            AddOutputConnector("Target", NodeType.TARGET);
        }

        public ActionData ExportData()
        {
            ActionData data = new ActionData()
            {
                type = GetRealId(Type, NodeType.ACTION).ToString(),
                param1 = GetParam(ParamId.PARAM_1),
                param2 = GetParam(ParamId.PARAM_2),
                param3 = GetParam(ParamId.PARAM_3),
                param4 = GetParam(ParamId.PARAM_4),
                param5 = GetParam(ParamId.PARAM_5),
                param6 = GetParam(ParamId.PARAM_6),
            };

            return data;
        }

        [Obsolete]
        public string ActionId { get; set; }
    }
}
