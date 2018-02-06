using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeAI.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_GAME_EVENT_START
    /// SMART_EVENT_GAME_EVENT_END
    /// </summary>
    [Node(MenuName = "Game event", Type = NodeType.EVENT_AGGRO, AllowedTypes = new NodeType[] { NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class GameEvent : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public GameEvent()
        {
            Type = NodeType.EVENT_AGGRO;

            //Update text
            NodeName.Content = "Game event";

            AddParam<GameEventSpecificType>(ParamId.PARAM_SPECIFIC_TYPE, "Type:");
            AddParam(ParamId.PARAM_1, "Game event:"); //TODO: Make general node!
        }

        enum GameEventSpecificType
        {
            EVENT_GAME_EVENT_START  = 68,
            EVENT_GAME_EVENT_END    = 69,
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new GameEvent();
        }
    }
}
