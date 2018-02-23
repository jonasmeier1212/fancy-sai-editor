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

namespace FancySaiEditor
{
    /// <summary>
    /// Interaktionslogik für SelectMysqlDatabase.xaml
    /// </summary>
    public partial class SelectMysqlDatabase
    {
        public SelectMysqlDatabase()
        {
            InitializeComponent();
        }

        private void TestConnection_Click(object sender, RoutedEventArgs e)
        {
            Database.CheckMysqlConnection(serverName.Text, port.Text, database.Text, username.Text, password.Password);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.MysqlServer = serverName.Text;
            Properties.Settings.Default.MysqlPort = port.Text;
            Properties.Settings.Default.MysqlUsername = username.Text;
            Properties.Settings.Default.MysqlPassword = password.Password;
            Properties.Settings.Default.MysqlWorldDatabase = database.Text;
            Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
