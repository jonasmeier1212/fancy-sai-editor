using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_KILL
    ///
    /// </summary>
    [Node(MenuName = "Kill", Type = NodeType.EVENT_AGGRO, AllowedTypes = new NodeType[] { NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class Kill : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public Kill()
        {
            Type = NodeType.EVENT_MANA_PTC;

            

            //Update text
            NodeName.Content = "Kill";

            AddParam(ParamId.PARAM_1, "Cooldown min");
            AddParam(ParamId.PARAM_2, "Cooldown max");
            AddParam<PlayerOnly>(ParamId.PARAM_3, "Player only");
            AddParam<GeneralNodes.Npc>(ParamId.PARAM_4, NodeType.GENERAL_NPC, "Creature");
        }

        enum PlayerOnly
        {
            NO,
            YES,
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new Kill();
        }
    }
}
