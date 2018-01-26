using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NodeAI.Nodes.ActionNodes
{
    [Node( MenuName = "ThreatSinglePct", Type = NodeType.ACTION_THREAT_SINGLE_PCT, AllowedTypes = new NodeType[] {NodeType.EVENT, NodeType.TARGET})]
    class ThreatSinglePct : ActionNode
    {
        public ThreatSinglePct()
        {
            Type = NodeType.ACTION_THREAT_SINGLE_PCT;

            ActionId = "13";

            //Update text
            NodeName.Content = "ThreatSinglePct";

            AddParam(ParamId.PARAM_1, "Increase Threat %");
            AddParam(ParamId.PARAM_2, "Decrease Threat %");
        }

        public override Node Clone()
        {
            return new ThreatSinglePct();
        }
    }
}
