using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancySaiEditor.Nodes.AIOwnerNodes
{
    [Node(MenuName = "Npc", Type = NodeType.AI_OWNER_NPC, AllowedTypes = new NodeType[] { NodeType.EVENT })]
    public class AIOwnerNode_Npc : AIOwner
    {
        public AIOwnerNode_Npc()
        {
            Type = NodeType.AI_OWNER_NPC;

            NodeName.Name = "Npc";

            
        }

        public override string GetName()
        {
            throw new NotImplementedException();
        }

        public override int GetEntryOrGuid()
        {
            throw new NotImplementedException();
        }

        public override Node Clone()
        {
            return new AIOwnerNode_Npc();
        }
    }
}
