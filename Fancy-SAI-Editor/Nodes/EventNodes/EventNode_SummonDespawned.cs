using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancySaiEditor.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_AGGRO
    ///
    /// </summary>
    [Node(MenuName = "Summon despawned", Type = NodeType.EVENT_SUMMON_DESPAWNED, AllowedTypes = new NodeType[] { NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class SummonDespawned : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public SummonDespawned()
        {
            Type = NodeType.EVENT_SUMMON_DESPAWNED;

            //Update text
            NodeName.Content = "Summon despawned";

            AddParam<GeneralNodes.Npc>(ParamId.PARAM_1, NodeType.GENERAL_NPC, "NPC");
            AddParam(ParamId.PARAM_2, "Cooldown min:");
            AddParam(ParamId.PARAM_3, "Cooldown max:");
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new SummonDespawned();
        }
    }
}
