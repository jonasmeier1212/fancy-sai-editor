using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancySaiEditor.Nodes.EventNodes
{
    /// <summary>
    /// SMART_EVENT_GAME_EVENT_START
    /// SMART_EVENT_GAME_EVENT_END
    /// </summary>
    [Node(MenuName = "Game event start", Type = NodeType.EVENT_GAME_EVENT_START, AllowedTypes = new NodeType[] { NodeType.GENERAL_NPC, NodeType.ACTION })]
    public class GameEventStart : EventNode
    {
        /// <summary>
        /// Standard constructor.
        /// Initializes type, node name, tooltips and adds the connectors.
        /// </summary>
        public GameEventStart()
        {
            Type = NodeType.EVENT_GAME_EVENT_START;

            //Update text
            NodeName.Content = "Game event start";

            AddParam(ParamId.PARAM_1, "Game event:"); //TODO: Make general node!
        }

        /// <summary>
        /// Clones the class instance.
        /// </summary>
        /// <returns>Returns clone of this class.</returns>
        public override Node Clone()
        {
            return new GameEventStart();
        }
    }
}
