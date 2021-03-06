﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancySaiEditor.Nodes.TargetNodes
{
    [Node(MenuName = "Owner or summoner", Type = NodeType.TARGET_OWNER_OR_SUMMONER, AllowedTypes = new NodeType[] { NodeType.ACTION })]
    public class OwnerOrSummoner : TargetNode
    {
        public OwnerOrSummoner()
        {
            Type = NodeType.TARGET_OWNER_OR_SUMMONER;

            

            NodeName.Content = "Owner or summoner";
        }

        public override Node Clone()
        {
            return new OwnerOrSummoner();
        }
    }
}
