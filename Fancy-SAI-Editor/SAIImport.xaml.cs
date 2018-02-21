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
            try
            {
                SAIImporter.ImportSAI(Convert.ToInt32(entry.Text));
            }
            catch(Exception)
            {
                MessageBox.Show($"Entry {entry.Text} is not a valid entry!");
            }
            Close();
        }

        private void entry_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                Convert.ToInt32(e.Text);
            }
            catch(FormatException)
            {
                e.Handled = true;
            }
        }
    }
}
