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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;

namespace FancySaiEditor
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// Initializes the MainWindow and the database and addes the MenuItems to the node selection menu.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            //Initialize node manager
            NodeManager.Instance = new NodeManager(NodeEditorCanvas);

            Database.DatabaseConnection.InitializeDatabase();

            CreateNodeCreationMenu();
        }

        private void CreateNodeCreationMenu()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            foreach (var type in assembly.GetTypes())
            {
                if (type.GetCustomAttribute<NodeAttribute>() != null)
                {
                    NodeMenuItem menuItem = new NodeMenuItem(type.GetCustomAttribute<NodeAttribute>().Type)
                    {
                        Header = type.GetCustomAttribute<NodeAttribute>().MenuName,
                    };
                    menuItem.Click += HandleNodeMenu;

                    if (type.IsSubclassOf(typeof(Nodes.ActionNodes.ActionNode)))
                    {
                        actionNodesMenu.Items.Add(menuItem);
                    }
                    else if (type.IsSubclassOf(typeof(Nodes.EventNodes.EventNode)))
                    {
                        eventNodesMenu.Items.Add(menuItem);
                    }
                    else if(type.IsSubclassOf(typeof(Nodes.TargetNodes.TargetNode)))
                    {
                        targetNodesMenu.Items.Add(menuItem);
                    }
                }
            }
        }

        private void HandleNodeMenu(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is NodeMenuItem item)
                    NodeManager.Instance.CreateNode(item.Type, item.Origin, item.CreatePosition);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error at node creation!\nError: " + exc.Message);
            }
        }   

        /// <summary>
        /// Shows a node selection menu in the editor at the mouse position with all node which has a connection possibilities with the passed NodeConnector.
        /// </summary>
        /// <param name="connector"></param>
        public void ShowNodeSelectionMenuInNodeEditor(NodeConnector connector)
        {
            ClearNodeEditorContexMenu();

            Assembly assembly = Assembly.GetExecutingAssembly();
            foreach (var type in assembly.GetTypes())
            {
                if (type.GetCustomAttribute<NodeAttribute>() == null || type.GetCustomAttribute<NodeAttribute>().AllowedTypes == null)
                    continue;

                if (connector.CanConnect(type.GetCustomAttribute<NodeAttribute>().Type) && (type.GetCustomAttribute<NodeAttribute>().AllowedTypes.Contains(connector.ParentNodeType) || type.GetCustomAttribute<NodeAttribute>().AllowedTypes.Contains(Node.GetSuperiorType(connector.ParentNodeType))))
                {
                    NodeMenuItem menuItem = new NodeMenuItem(type.GetCustomAttribute<NodeAttribute>().Type, connector.ParentNode)
                    {
                        Header = type.GetCustomAttribute<NodeAttribute>().MenuName,
                    };
                    menuItem.Click += HandleNodeMenu;

                    if (type.Namespace.Contains("AIOwnerNodes"))
                        selectionMenuOwnerNodes.Items.Add(menuItem);
                    else if (type.Namespace.Contains("ActionNodes"))
                        selectionMenuActionNodes.Items.Add(menuItem);
                    else if (type.Namespace.Contains("EventNodes"))
                        selectionMenuEventNodes.Items.Add(menuItem);
                    else if (type.Namespace.Contains("TargetNodes"))
                        selectionMenuTargetNodes.Items.Add(menuItem);
                }
            }
        }

        /// <summary>
        /// Shows a node selection menu in the editor at the mouse position which allows to create any node at the mouse position
        /// </summary>
        /// <param name="connector"></param>
        public void ShowNodeSelectionMenuInNodeEditor(Point mousePosition)
        {
            ClearNodeEditorContexMenu();

            Assembly assembly = Assembly.GetExecutingAssembly();
            foreach (var type in assembly.GetTypes())
            {
                if (type.GetCustomAttribute<NodeAttribute>() == null)
                    continue;

                NodeMenuItem menuItem = new NodeMenuItem(type.GetCustomAttribute<NodeAttribute>().Type, mousePosition)
                {
                    Header = type.GetCustomAttribute<NodeAttribute>().MenuName,
                };
                menuItem.Click += HandleNodeMenu;
                if (type.Namespace.Contains("AIOwnerNodes"))
                    selectionMenuOwnerNodes.Items.Add(menuItem);
                else if (type.Namespace.Contains("ActionNodes"))
                    selectionMenuActionNodes.Items.Add(menuItem);
                else if (type.Namespace.Contains("EventNodes"))
                    selectionMenuEventNodes.Items.Add(menuItem);
                else if (type.Namespace.Contains("TargetNodes"))
                    selectionMenuTargetNodes.Items.Add(menuItem);
            }
        }

        private void HandleKeyboard(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Delete:
                    NodeManager.Instance.DeleteNodes();
                    break;
                case Key.C:
                    if(Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                        NodeManager.Instance.CopyNodes();
                    break;
                case Key.V:
                    if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                        NodeManager.Instance.PasteNodes();
                    break;
                case Key.A:
                    if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                        NodeManager.Instance.SelectAllNodes();
                    break;
                default:
                    break;
            }
        }

        private void HandlePositionNodes(object sender, RoutedEventArgs e)
        {
            NodeManager.Instance.Position();
        }

        private void HandleContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            if(!(e.Source is Canvas) && !(e.OriginalSource is Ellipse))
            {
                e.Handled = true;
                return;
            }

            if (selectionMenuEventNodes.Items.IsEmpty)
                selectionMenuEventNodes.Visibility = Visibility.Collapsed;
            else
                selectionMenuEventNodes.Visibility = Visibility.Visible;

            if (selectionMenuActionNodes.Items.IsEmpty)
                selectionMenuActionNodes.Visibility = Visibility.Collapsed;
            else
                selectionMenuActionNodes.Visibility = Visibility.Visible;

            if (selectionMenuTargetNodes.Items.IsEmpty)
                selectionMenuTargetNodes.Visibility = Visibility.Collapsed;
            else
                selectionMenuTargetNodes.Visibility = Visibility.Visible;

            if (selectionMenuOwnerNodes.Items.IsEmpty)
                selectionMenuOwnerNodes.Visibility = Visibility.Collapsed;
            else
                selectionMenuOwnerNodes.Visibility = Visibility.Visible;

            if (selectionMenuEventNodes.Items.IsEmpty && selectionMenuActionNodes.Items.IsEmpty && selectionMenuTargetNodes.Items.IsEmpty && selectionMenuOwnerNodes.Items.IsEmpty)
                e.Handled = true;
        }

        private void HandleContextMenuClosing(object sender, ContextMenuEventArgs e)
        {
            //Clear the content of the node selection context menu to prevent wrong items at next opening
            ClearNodeEditorContexMenu();
        }

        private void ClearNodeEditorContexMenu()
        {
            selectionMenuTargetNodes.Items.Clear();
            selectionMenuActionNodes.Items.Clear();
            selectionMenuEventNodes.Items.Clear();
            selectionMenuOwnerNodes.Items.Clear();
        }

        private void HandleRightClick(object sender, MouseButtonEventArgs e)
        {
            //Show node creation menu
            if(e.Source is Canvas)
                ShowNodeSelectionMenuInNodeEditor(e.GetPosition(null));
        }

        private void Window_LayoutUpdated(object sender, EventArgs e)
        {
            NodeManager.Instance.Update();
            if (NodeEditorCanvas.ActualWidth != 0 && NodeEditorCanvas.ActualHeight != 0 && normalSize == default(Point))
                normalSize = new Point(NodeEditorCanvas.ActualWidth, NodeEditorCanvas.ActualHeight);
            ResizeNodeEditor();
        }

        private Point dragAnchorPoint;
        private Point selectAnchorPoint;
        private Rectangle selectionRect;

        private void NodeEditorCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if(Mouse.LeftButton == MouseButtonState.Pressed && e.OriginalSource == NodeEditorCanvas)
                SelectMany();

            if (Mouse.MiddleButton == MouseButtonState.Pressed && e.OriginalSource == NodeEditorCanvas)
                DragEditor(e);
        }

        private void NodeEditorCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NodeManager.Instance.DeselectNodes();
            InitSelectMany();
        }

        private void NodeEditorCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            EndSelectMany();
        }

        private void MenutItemExport_Click(object sender, RoutedEventArgs e)
        {
            NodeManager.Instance.Export();
        }

        private void MenuItemImportSAI_Click(object sender, RoutedEventArgs e)
        {
            SAIImport import = new SAIImport();
            import.ShowDialog();
        }

        private void InitDragEditor(MouseButtonEventArgs e)
        {
            dragAnchorPoint = e.GetPosition(null);
        }

        private void DragEditor(MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.ScrollAll;

            Point curPoint = e.GetPosition(null);

            double offsetX = dragAnchorPoint.X - curPoint.X;
            double offsetY = dragAnchorPoint.Y - curPoint.Y;

            //Check if offset is greater than minimum offset
            //This must be done before scaling the offset because this config value
            //should be independent of the panning ratio
            if (Math.Abs(offsetX) < Properties.Settings.Default.PanningMinOffset)
                offsetX = 0;
            if (Math.Abs(offsetY) < Properties.Settings.Default.PanningMinOffset)
                offsetY = 0;

            //Scale offset to right value
            offsetX = NodeEditorScrollViewer.ContentHorizontalOffset + offsetX * Properties.Settings.Default.PanningRatio;
            offsetY = NodeEditorScrollViewer.ContentVerticalOffset + offsetY * Properties.Settings.Default.PanningRatio;

            //Check if the horizontal offset exceeds the actual scrollable content
            if (offsetX > NodeEditorScrollViewer.ScrollableWidth)
            {
                //Enlarge editor canvas horizontally
                NodeEditorCanvas.Width = NodeEditorCanvas.ActualWidth + offsetX / Properties.Settings.Default.PanningRatio - NodeEditorScrollViewer.ContentHorizontalOffset;
                UpdateLayout();
            }
            //Check if the vertival offset exceeds the actual scrollable content
            if (offsetY > NodeEditorScrollViewer.ScrollableHeight)
            {
                //Enlarge editor canvas vertically
                NodeEditorCanvas.Height = NodeEditorCanvas.ActualHeight + offsetY / Properties.Settings.Default.PanningRatio - NodeEditorScrollViewer.ContentVerticalOffset;
                UpdateLayout();
            }

            //TODO: Diminish canvas size if there is no content in the part outside the screen

            NodeEditorScrollViewer.ScrollToHorizontalOffset(offsetX);
            NodeEditorScrollViewer.ScrollToVerticalOffset(offsetY);

            dragAnchorPoint = curPoint;
        }

        private void EndDragEditor()
        {
            //Reset cursor
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void InitSelectMany()
        {
            selectAnchorPoint = Mouse.GetPosition(null);

            if (selectAnchorPoint.X < 0 || selectAnchorPoint.Y < 0) //Sometimes after rightclick the mouse position is negativ I don't know why... -> Prevent this
                selectAnchorPoint = default(Point);

            selectionRect = new Rectangle()
            {
                Stroke = Brushes.Aquamarine,
            };
            Canvas.SetLeft(selectionRect, selectAnchorPoint.X);
            Canvas.SetTop(selectionRect, selectAnchorPoint.Y);
            NodeEditorCanvas.Children.Add(selectionRect);
        }

        private void SelectMany()
        {
            if (selectAnchorPoint == default(Point) || selectionRect == null)
                return;

            Point curPoint = Mouse.GetPosition(null);

            double offsetX = selectAnchorPoint.X - curPoint.X;
            double offsetY = selectAnchorPoint.Y - curPoint.Y;

            if(offsetX > 0)
                Canvas.SetLeft(selectionRect, curPoint.X);
            if (offsetY > 0)
                Canvas.SetTop(selectionRect, curPoint.Y);

            selectionRect.Width = Math.Abs(offsetX);
            selectionRect.Height = Math.Abs(offsetY);
        }

        private void EndSelectMany()
        {
            if (selectAnchorPoint != null && selectionRect != null)
                NodeManager.Instance.SelectNodesInRect(selectionRect);

            selectAnchorPoint = default(Point);
            NodeEditorCanvas.Children.Remove(selectionRect);
            selectionRect = null;
        }

        public bool IsSelecting()
        {
            if (selectAnchorPoint != default(Point) && selectionRect != null)
                return true;

            return false;
        }

        private Point normalSize;

        private void ResizeNodeEditor()
        {
            double height = normalSize.Y;
            double width = normalSize.X;

            foreach (FrameworkElement element in NodeEditorCanvas.Children)
            {
                double x = Canvas.GetLeft(element);
                double y = Canvas.GetTop(element);
                if (!Double.IsNaN(x) && !Double.IsNaN(y))
                {
                    if (x + element.ActualWidth > width)
                        width = x + element.ActualWidth;
                    if (y + element.ActualHeight > height)
                        height = y + element.ActualHeight;
                }
            }

            NodeEditorCanvas.MinHeight = height;
            NodeEditorCanvas.MinWidth = width;
        }

        private void NodeEditorCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            EndDragEditor();

            Mouse.OverrideCursor = Cursors.Arrow;
            NodeEditorCanvas.Children.Remove(selectionRect);
            selectionRect = null;
        }

        private void NodeEditorCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle)
                InitDragEditor(e);
        }

        private void NodeEditorCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle)
                EndDragEditor();
        }
    }
}
