﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancySaiEditor.Nodes.TargetNodes
{
    [Node(MenuName = "Threat list", Type = NodeType.TARGET_THREAT_LIST, AllowedTypes = new NodeType[] { NodeType.ACTION })]
    public class ThreatList : TargetNode
    {
        public ThreatList()
        {
            Type = NodeType.TARGET_THREAT_LIST;

            

            NodeName.Content = "Threat list";
        }

        public override Node Clone()
        {
            return new ThreatList();
        }
    }
}
