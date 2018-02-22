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
    /// Interaktionslogik für ShowSqlExport.xaml
    /// </summary>
    public partial class ShowSqlExport : Window
    {
        public ShowSqlExport(String sqlText)
        {
            InitializeComponent();
            ExportText.Text = sqlText;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
