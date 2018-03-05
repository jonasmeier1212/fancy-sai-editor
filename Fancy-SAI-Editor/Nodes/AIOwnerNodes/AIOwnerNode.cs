using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FancySaiEditor.Nodes.AIOwnerNodes
{
    public abstract class AIOwner : Node
    {
        public AIOwner()
        {
            AddOutputConnector("Event:", NodeType.EVENT);
        }

        public abstract int GetEntryOrGuid();

        public abstract string GetName();
    }
}
