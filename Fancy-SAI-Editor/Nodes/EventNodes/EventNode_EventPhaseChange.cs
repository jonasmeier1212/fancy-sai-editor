using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancySaiEditor.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_PHASE_CHANGED
    ///
    /// </summary>
    [Node(MenuName = "Dummy Effect", Type = NodeType.EVENT_DUMMY_EFFECT, AllowedTypes = new NodeType[] { NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class DummyEffect : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public DummyEffect()
        {
            Type = NodeType.EVENT_DUMMY_EFFECT;

            //Update text
            NodeName.Content = "Event phase changed";

            AddParam<GeneralNodes.Spell>(ParamId.PARAM_1, NodeType.GENERAL_SPELL, "Spell");
            AddParam(ParamId.PARAM_2, "Effect index:");
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new DummyEffect();
        }
    }
}
