using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Data;

namespace NodeAI.Nodes.GeneralNodes
{
    /// <summary>
    /// Base class for every general node
    /// </summary>
    public abstract class GeneralNode : Node
    {
        public GeneralNode()
        {
            AddOutputConnector("Out", NodeType.NONE, int.MaxValue);

            additionalTab = null;
            valueTexts = new List<TextBlock>();
        }

        public abstract string GetParamValue();

        public string AdditionalQuery { get; set; }

        private StackPanel selectPanel; //The select panel is shown if there is no database data selected and is used to open a windows in which the user can select data from the database
        private StackPanel dataPanel; //The data panel is shown if there is data selected
        private List<TextBlock> valueTexts;

        private AdditionalTab additionalTab;
        private DataSelectionPossibility selectionPossibility;

        /// <summary>
        /// Adds a frame for selecting data, specified by the nodeData, from the database
        /// and a frame which shows the selected data.
        /// </summary>
        protected void AddDatabaseSelectionFrame(DataSelectionPossibility _selectionPossibility, AdditionalTab _additionalTab = null)
        {
            additionalTab = _additionalTab;
            selectionPossibility = _selectionPossibility;
            #region Select Panel
            selectPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };
            Label text = new Label
            {
                Content = NodeName.Content + ":",
                Foreground = Brushes.White,
            };
            Button searchButton = new Button
            {
                Content = "...",
                Padding = new Thickness(8, 0, 8, 0),
                Margin = new Thickness(0, 0, 5, 0),
            };
            searchButton.Click += OpenSelectionWindow;
            selectPanel.Children.Add(text);
            selectPanel.Children.Add(searchButton);
            nodeMainPanel.Children.Add(selectPanel);
            #endregion

            #region Data Panel
            dataPanel = new StackPanel
            {
                Visibility = Visibility.Collapsed,
            };
            Grid dataGrid = new Grid();
            dataGrid.ColumnDefinitions.Add(new ColumnDefinition());
            dataGrid.ColumnDefinitions.Add(new ColumnDefinition());

            int i = 0;
            foreach (DataColumn column in NodeData.Columns)
            {
                dataGrid.RowDefinitions.Add(new RowDefinition());
                Label identifierLable = new Label()
                {
                    Content = column.ColumnName,
                    Foreground = Brushes.White,
                };
                Grid.SetColumn(identifierLable, 0);
                Grid.SetRow(identifierLable, i);

                valueTexts.Add(new TextBlock()
                {
                    Foreground = Brushes.White,
                    MaxWidth = 130,
                    TextWrapping = TextWrapping.WrapWithOverflow,
                    VerticalAlignment = VerticalAlignment.Center,
                });

                if(column.ColumnName == "Text")
                {
                    ScrollViewer scrollViewer = new ScrollViewer()
                    {
                        MaxHeight = 70,
                    };
                    scrollViewer.Content = valueTexts.Last();
                    Grid.SetColumn(scrollViewer, 1);
                    Grid.SetRow(scrollViewer, i);
                    dataGrid.Children.Add(scrollViewer);
                }
                else
                {
                    Grid.SetColumn(valueTexts.Last(), 1);
                    Grid.SetRow(valueTexts.Last(), i);
                    dataGrid.Children.Add(valueTexts.Last());
                }
                
                dataGrid.Children.Add(identifierLable);

                ++i;
            }

            dataPanel.Children.Add(dataGrid);
            nodeMainPanel.Children.Add(dataPanel);
            #endregion
        }

        public void SelectData(DataGrid data)
        {
            try
            {
                DataRow selectedRow = (data.SelectedItem as DataRowView).Row;
                int i = 0;
                foreach (TextBlock valueText in valueTexts)
                {
                    valueText.Text = selectedRow.ItemArray[i].ToString();
                    ++i;
                }

                selectPanel.Visibility = Visibility.Collapsed;
                dataPanel.Visibility = Visibility.Visible;
                MainWindow.UpdateLayout();
                MainWindow.UpdateNodeConnections();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Unknown error!\nError Message: " + exc.Message);
            }
        }

        public void SelectData(NodeData data)
        {
            try
            {
                DataRow selectedRow = data.Rows[0] as DataRow;
                int i = 0;
                foreach (TextBlock valueText in valueTexts)
                {
                    valueText.Text = selectedRow.ItemArray[i].ToString();
                    ++i;
                }

                selectPanel.Visibility = Visibility.Collapsed;
                dataPanel.Visibility = Visibility.Visible;
                MainWindow.UpdateLayout();
                MainWindow.UpdateNodeConnections();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Unknown error!\nError Message: " + exc.Message);
            }
        }

        private void OpenSelectionWindow(object sender, RoutedEventArgs e)
        {
            DataWindow dataWindow = new DataWindow(this);

            dataWindow.AddSelectionPossibility(selectionPossibility);

            CreatureTextCreation creatureTextCreation = new CreatureTextCreation();

            if (additionalTab != null)
            {
                TabItem item = new TabItem()
                {
                    Header = additionalTab.Header,
                };
                item.Content = additionalTab.Content;
                dataWindow.TabControl.Items.Add(item);
            }

            dataWindow.ShowDialog();
        }
    }
}
