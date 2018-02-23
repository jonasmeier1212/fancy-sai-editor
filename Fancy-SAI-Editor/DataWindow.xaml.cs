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

namespace NodeAI
{
    public partial class DataWindow
    {
        /// <summary>
        /// Initializes the DataWindow.
        /// </summary>
        public DataWindow(Node _creatorNode)
        {
            InitializeComponent();
            creatorNode = _creatorNode;
            selectionPossibilties = new List<DataSelectionPossibility>();
        }

        private Node creatorNode;
        private List<DataSelectionPossibility> selectionPossibilties;

        public Node CreatorNode { get => creatorNode; }

        private async void HandleSearch(object sender, RoutedEventArgs e)
        {
            try
            {
                DataSelectionPossibility selectionPossibility = selectionPossibilties[SearchElementType.SelectedIndex];

                Debug.Assert(CreatorNode.NodeData.Columns.Contains(selectionPossibility.SelectColumn), "No data column matches the select column name!");

                if(CreatorNode.NodeData.Sqlite)
                    await Database.SelectSqliteData(selectionPossibility.SelectColumn, SearchTerm.Text, CreatorNode.NodeData);
                else
                    await Database.SelectMySqlData(selectionPossibility.SelectColumn, SearchTerm.Text, CreatorNode.NodeData);
                SearchData.ItemsSource = CreatorNode.NodeData.DefaultView;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Something went wrong at searching data\nError:" + exc.Message);
            }
        }

        /// <summary>
        /// Event Handler for selecting the data from the DataGrid in which the data loaded from the Database is shown.
        /// </summary>
        private void SelectData(object sender, MouseButtonEventArgs e)
        {
            if (creatorNode is Nodes.GeneralNodes.GeneralNode generalNode && sender is DataGrid data)
                generalNode.SelectData(data);
            Close();
        }

        private void HandleSearchTerm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                HandleSearch(sender, new RoutedEventArgs());
        }

        /// <summary>
        /// Adds a possibility for the user to select data from database.
        /// </summary>
        /// <param name="_selectColumn">Name of database column to search for.</param>
        /// <param name="_selectionName">Text shown in the select panel for this select field</param>
        public void AddSelectionPossibility(DataSelectionPossibility dataSelectionPossibility)
        {
            selectionPossibilties.Add(dataSelectionPossibility);
            SearchElementType.Items.Add(new ComboBoxItem()
            {
                IsSelected = true,
                Content = dataSelectionPossibility.SelectionName,
            });
        }
    }

    public struct DataSelectionPossibility
    {
        public DataSelectionPossibility(string selectionName, string selectColumn) : this()
        {
            SelectionName = selectionName;
            SelectColumn = selectColumn;
        }

        public string SelectionName { get; set; }
        public string SelectColumn { get; set; }
    }

    public class AdditionalTab
    {
        public AdditionalTab(UserControl content, string header)
        {
            Content = content ?? throw new ArgumentNullException(nameof(content));
            Header = header ?? throw new ArgumentNullException(nameof(header));
        }

        public UserControl Content;
        public string Header;
    }
}
