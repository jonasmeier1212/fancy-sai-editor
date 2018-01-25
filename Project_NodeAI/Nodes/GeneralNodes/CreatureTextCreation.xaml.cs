using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NodeAI.Nodes.GeneralNodes
{
    /// <summary>
    /// Interaktionslogik für CreatureTextCreation.xaml
    /// </summary>
    public partial class CreatureTextCreation : UserControl
    {
        public CreatureTextCreation()
        {
            InitializeComponent();

            broadcastData = new BroadcastTextData();
            broadcastSelector.ItemsSource = broadcastData.DefaultView;
        }

        private BroadcastTextData broadcastData;

        private async void SearchBroadcastText(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (sender is TextBox input)
                {
                    await Database.SelectBroadcastText(input.Text, broadcastData);
                    broadcastSelector.ItemsSource = broadcastData.DefaultView;
                }
            }
        }

        private void SelectBroadcastText(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DataGrid dataGrid = sender as DataGrid;
                DataRow selectedRow = (dataGrid.SelectedItem as DataRowView).Row;
                broadcastTextId.Text = selectedRow[0].ToString();
                if (selectedRow[2].ToString() != "")
                    text.Text = selectedRow[2].ToString();
                else
                    text.Text = selectedRow[3].ToString();
                language.Text = selectedRow[1].ToString();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error at selecting broadcast text\nError: " + exc.Message);
            }
        }

        private void CreateText(object sender, RoutedEventArgs e)
        {
            try
            {
                DataWindow dataWindow = Utility.GetParentWithType<DataWindow>(sender);
                if (dataWindow.CreatorNode is GeneralNodes.Text textNode)
                {
                    textNode.AdditionalQuery =
                        "DELETE FROM `creature_text` WHERE `entry`=@NPC;\n" +
                        "INSERT INTO `creature_text` (`entry`, `groupid`, `id`, `text`, `type`, `language`, `probability`, `emote`, `duration`, `sound`, `comment`) VALUES\n" +
                        "(@NPC, " + groupID.Text + ", " + id.Text + ", '" + text.Text + "', " + (type.SelectedItem as ComboBoxItem).Uid + ", " + language.Text + ", " + "100" + ", " + emote.Text + ", " + duration.Text + ", " + sound.Text + ", '');\n";

                    dataWindow.CreatorNode.NodeData.Clear();
                    DataRow newRow = dataWindow.CreatorNode.NodeData.NewRow();
                    newRow["GroupId"] = groupID.Text;
                    newRow["Text"] = text.Text;
                    newRow["Type"] = type.Text;
                    dataWindow.CreatorNode.NodeData.Rows.Add(newRow);

                    textNode.SelectData(dataWindow.CreatorNode.NodeData);
                }
                dataWindow.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error at creating text!\nError: " + exc.Message);
            }
        }
    }
}
