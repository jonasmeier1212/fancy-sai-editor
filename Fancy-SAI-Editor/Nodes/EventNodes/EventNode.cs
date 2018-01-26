using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.EventNodes
{
    /// <summary>
    /// Base class for every event node
    /// </summary>
    public abstract class EventNode : Node
    {
        public EventNode()
        {
            AddInputConnector("NPC", NodeType.GENERAL_NPC);
            AddOutputConnector("Action", NodeType.ACTION, int.MaxValue);
        }

        public EventData ExportData()
        {
            EventData data = new EventData
            {
                param1 = GetParam(ParamId.PARAM_1),
                param2 = GetParam(ParamId.PARAM_2),
                param3 = GetParam(ParamId.PARAM_3),
                param4 = GetParam(ParamId.PARAM_4)
            };
            if (GetParam(ParamId.PARAM_SPECIFIC_TYPE) != "")
                data.type = GetParam(ParamId.PARAM_SPECIFIC_TYPE);
            else
                data.type = EventId;

            return data;
        }

        public string EventId { get; set; }
    }
}
