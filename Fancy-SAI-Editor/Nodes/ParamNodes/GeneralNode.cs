using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Data;

namespace FancySaiEditor.Nodes.ParamNodes
{
    /// <summary>
    /// Base class for every param node
    /// </summary>
    public abstract class ParamNode : Node
    {
        public ParamNode()
        {
            AddOutputConnector("Out", NodeType.NONE, int.MaxValue);
        }

        public abstract string GetParamValue();

        public abstract void SetParamValue(string value);

        public string AdditionalQuery { get; set; }
    }
}
