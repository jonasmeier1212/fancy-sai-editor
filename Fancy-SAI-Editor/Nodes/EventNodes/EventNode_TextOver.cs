using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_TEXT_OVER
    ///
    /// </summary>
    [Node(MenuName = "Text over", Type = NodeType.EVENT_TEXT_OVER, AllowedTypes = new NodeType[] { NodeType.GENERAL_TEXT, NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class TextOver : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public TextOver()
        {
            Type = NodeType.EVENT_TEXT_OVER;

            

            //Update text
            NodeName.Content = "Text over";

            AddParam<GeneralNodes.Text>(ParamId.PARAM_1, NodeType.GENERAL_TEXT, "Text");
            AddParam<GeneralNodes.Npc>(ParamId.PARAM_2, NodeType.GENERAL_NPC, "NPC");
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new TextOver();
        }
    }
}
