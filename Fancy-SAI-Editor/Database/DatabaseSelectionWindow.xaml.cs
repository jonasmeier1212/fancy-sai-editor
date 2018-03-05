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
using System.Data;
using System.Diagnostics;

namespace FancySaiEditor.Database
{
    public delegate void SelectDataCallback(DataRow data);

    public partial class DatabaseSelectionWindow
    {
        /// <summary>
        /// Initializes the DataWindow.
        /// </summary>
        public DatabaseSelectionWindow(NodeData _nodeData, SelectDataCallback _dataCallback)
        {
            InitializeComponent();
            nodeData = _nodeData;
            Debug.Assert(_dataCallback != null, "SelectDataCallback must not be null!");
            dataCallback = _dataCallback;

            //Generate possible selections from node data columns
            foreach(DataColumn column in nodeData.Columns)
            {
                SearchElementType.Items.Add(column.ColumnName);
            }
            SearchElementType.SelectedIndex = 0;
        }

        private NodeData nodeData;
        private SelectDataCallback dataCallback;

        private async void Search(string selectColumn, string searchTerm)
        {
            if(nodeData.Sqlite)
                await DatabaseConnection.SelectSqliteData(selectColumn, searchTerm, nodeData);
            else
                await DatabaseConnection.SelectMySqlData(selectColumn, searchTerm, nodeData);
            SearchData.ItemsSource = nodeData.DefaultView;
        }

        private void SelectDataRow(DataRow dataRow)
        {
            dataCallback(dataRow);
            Close();
        }

        private void SearchTerm_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if(e.Key == Key.Enter)
                    Search(SearchElementType.SelectedValue.ToString(), SearchTerm.Text);
            }
            catch (Exception exc)
            {
                MessageBox.Show($"Unknow error in { exc.Source.ToString()}!" +
                    $"\nError message: {exc.Message}");
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Search(SearchElementType.SelectedValue.ToString(), SearchTerm.Text);
            }
            catch (Exception exc)
            {
                MessageBox.Show($"Unknow error in { exc.Source.ToString()}!" +
                    $"\nError message: {exc.Message}");
            }
        }

        private void SearchData_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                SelectDataRow(((sender as DataGrid).SelectedItem as DataRowView).Row);
            }
            catch (Exception exc)
            {
                MessageBox.Show($"Unknow error in { exc.Source.ToString()}!" +
                    $"\nError message: {exc.Message}");
            }
        }
    }
}
