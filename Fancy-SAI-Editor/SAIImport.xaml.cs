using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NodeAI
{
    /// <summary>
    /// Interaktionslogik für SAIImport.xaml
    /// </summary>
    public partial class SAIImport : Window
    {
        public SAIImport()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SAIImporter.ImportSAI(Convert.ToInt32(entry.Text));
            this.Close();
        }
    }
}
