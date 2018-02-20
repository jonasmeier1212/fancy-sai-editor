using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NodeAI.Nodes.ActionNodes
{
    [Node(MenuName = "Send custom anim", Type = NodeType.ACTION_SEND_GO_CUSTOM_ANIM, AllowedTypes = new NodeType[] { NodeType.EVENT, NodeType.TARGET })]
    class SendCustomAnim : ActionNode
    {
        public SendCustomAnim()
        {
            Type = NodeType.ACTION_SEND_GO_CUSTOM_ANIM;

            //Update text
            NodeName.Content = "Send custom anim";

            AddParam(ParamId.PARAM_1, "Anim ID:");
        }

        public override Node Clone()
        {
            return new SendCustomAnim();
        }
    }
}
