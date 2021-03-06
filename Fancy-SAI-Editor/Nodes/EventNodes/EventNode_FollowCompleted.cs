﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancySaiEditor.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_FOLLOW_COMPLETED
    ///
    /// </summary>
    [Node(MenuName = "Follow completed", Type = NodeType.EVENT_FOLLOW_COMPLETED, AllowedTypes = new NodeType[] { NodeType.AI_OWNER, NodeType.ACTION })]
    public class FollowCompleted : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public FollowCompleted()
        {
            Type = NodeType.EVENT_FOLLOW_COMPLETED;

            

            //Update text
            NodeName.Content = "Follow completed";
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new FollowCompleted();
        }
    }
}
