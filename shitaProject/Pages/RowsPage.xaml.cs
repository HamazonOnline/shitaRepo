using BLL;
using DTO;
using MaterialDesignThemes.Wpf;
using shitaProject.Windows;
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

namespace shitaProject.Pages
{
    /// <summary>
    /// Interaction logic for RowsPage.xaml
    /// </summary>
    public partial class RowsPage : Page
    {
        int columnId;
        List<RowWithLinesDTO> Rows;
        public RowsPage(ColumnDTO column)
        {
            InitializeComponent();

            columnId = column.Id;
            columnLbl.Content = $"דלת {column.Name}";
            GetRows();
            SetRows();
        }

        public void GetRows()
        {
            Rows = BLL.LocationServise.instance.GetRowsPerColumn(columnId).OrderByDescending(x => x.Name).ToList();
        }

        public void SetRows()
        {
            int margin = 10 * 8;
            //remove all children from rowsSp
            RowsSp.Children.Clear();
            StackPanel rowSp;
            foreach (var item in Rows)
            {
                Border border = new Border();
                //put the stack panel in the border
                border.BorderThickness = new Thickness(0, 0, 0, 5);
                border.BorderBrush = Brushes.Black;
                rowSp = new StackPanel();
                rowSp.Tag =  //item.Id;
                border.Child = rowSp;
                rowSp.Orientation = Orientation.Horizontal;
                rowSp.Margin = new Thickness(0, 10, 0, 10);

                //set rowSp tag as binding to item
                rowSp.Tag = item;


                int linesCounter = item.Lines.Count;
                foreach (var item2 in item.Lines)
                {
                    int width = (1100 - (10 * linesCounter)) / linesCounter;

                    //PopupBox popup = new PopupBox();
                    //popup.Content = "עריכת מוצר";
                    //popup.MouseDoubleClick += EditLine;



                    Button b = new Button();
                    //set popup to button b
                    // b
                    b.Content = item2.ProductName != null ? item2.ProductName : "";
                    b.BorderBrush = item2.ProductId != null ? Brushes.Red : Brushes.Black;
                    b.Tag = item2;//.Id;

                    b.Width = width;
                    b.Margin = new Thickness(0, 0, 10, 0);
                    b.Click += SelectLine;
                    rowSp.Children.Add(b);
                }

                Label label = new Label();
                label.Content = item.Name;
                label.FontSize = 20;

                rowSp.Children.Add(label);

                PackIcon icon = new PackIcon();
                icon.Kind = PackIconKind.Add;
                icon.Foreground = new SolidColorBrush(Colors.White);
                icon.Height = 30;
                icon.Width = 30;
                icon.HorizontalAlignment = HorizontalAlignment.Center;
                icon.VerticalAlignment = VerticalAlignment.Center;

                Style myStyle = (Style)Resources["AddRemoveStyle"];
                Button addButton = new Button();
                addButton.Content = icon;// "הוספת שורה";
                //set to buttom material design icon as add
                addButton.Background = Brushes.LightGreen;
                addButton.Tag = item.Id;
                addButton.Width = 50;
                addButton.Margin = new Thickness(50, 0, 0, 0);
                addButton.Click += AddLine;
                addButton.Style = myStyle;
                rowSp.Children.Add(addButton);

                icon = new PackIcon();
                icon.Kind = PackIconKind.Delete;
                icon.Foreground = new SolidColorBrush(Colors.White);
                icon.Height = 30;
                icon.Width = 30;
                icon.HorizontalAlignment = HorizontalAlignment.Center;
                icon.VerticalAlignment = VerticalAlignment.Center;

                addButton = new Button();
                addButton.Content = icon;// "הוספת שורה";
                //set to buttom material design icon as add
                addButton.Background = Brushes.LightCoral;
                addButton.Tag = item.Id;
                addButton.Width = 50;
                addButton.Margin = new Thickness(10, 0, 0, 0);
                addButton.Click += RemoveLine;
                addButton.Style = myStyle;
                rowSp.Children.Add(addButton);

                icon = new PackIcon();
                icon.Kind = PackIconKind.Edit;
                icon.Foreground = new SolidColorBrush(Colors.White);
                icon.Height = 30;
                icon.Width = 30;
                icon.HorizontalAlignment = HorizontalAlignment.Center;
                icon.VerticalAlignment = VerticalAlignment.Center;

                addButton = new Button();
                addButton.Content = icon;// "הוספת שורה";
                //set to buttom material design icon as add
                addButton.Background = Brushes.LightPink;
                addButton.Tag = item.Id;
                addButton.Width = 50;
                addButton.Margin = new Thickness(10, 0, 0, 0);
                addButton.Click += EditLine;
                addButton.Style = myStyle;
                rowSp.Children.Add(addButton);

                RowsSp.Children.Add(border);
            }
        }

        private void EditLine(object sender, RoutedEventArgs e)
        {
            if (selectedButton == null)
            {
                (new AutoCloseMessageBox(4, "יש לבחור שורה למחיקה", false)).Show();
                return;
            }
            LineDTO line = (selectedButton.Tag as LineDTO);
            (sender as Button).Background = Brushes.LightGray;
            selectedButton = null;
            string rowName = ((((sender as Button).Parent as StackPanel).Tag) as RowWithLinesDTO).Name;
            string producttMame = !string.IsNullOrEmpty((sender as Button).Content.ToString()) ? (sender as Button).Content.ToString() : "";
            SetLineDetailsWindow win = new SetLineDetailsWindow(producttMame, rowName, line.Name, line.Id);
            win.Show();
        }

        Button selectedButton = null;
        private void SelectLine(object sender, RoutedEventArgs e)
        {
            if (selectedButton == null)
            {
                //set button background as #FFC0C0
                (sender as Button).Background = new SolidColorBrush(Colors.LightBlue);
                selectedButton = (sender as Button);
            }
            else if (selectedButton == (sender as Button))
            {//set button foreground as  default color

                (sender as Button).Background = Brushes.LightGray;
                selectedButton = null;
            }
            else
            {
                selectedButton.Background = Brushes.LightGray;
                (sender as Button).Background = new SolidColorBrush(Colors.LightBlue);
                selectedButton = (sender as Button);
            }
        }

        private void AddLine(object sender, RoutedEventArgs e)
        {
            int rowId = (int)((sender as Button).Tag);
            RowWithLinesDTO selectedRow = Rows.FirstOrDefault(x => x.Id == rowId);
            if (selectedRow.Lines.Count == CurrentData.instance.MaxLinesPerRow)
            {
                MessageBox.Show($"לא ניתן להוסיף יותר מ {CurrentData.instance.MaxLinesPerRow} שורות"); return;
            }
            LineDTO newLine = LocationServise.instance.AddLine(rowId);
            selectedRow.Lines.Add(newLine);
            SetRows();
        }

        private void RemoveLine(object sender, RoutedEventArgs e)
        {
            LineDTO line;
            int rowId = (int)((sender as Button).Tag);
            RowWithLinesDTO selectedRow = Rows.FirstOrDefault(x => x.Id == rowId);
            if (selectedRow.Lines.Count == 1)
            {
                MessageBox.Show($"חריגה מכמות מינימום למדף"); return;
            }
            //check if all lines productBarcode is null or empty
            if (selectedRow.Lines.All(x => x.ProductId == null))
            {
                //remove from lines the item with the highest id
                line = selectedRow.Lines.OrderByDescending(x => x.Name).FirstOrDefault();

            }
            else
            {
                if (selectedButton == null)
                {
                    (new AutoCloseMessageBox(4, "יש לבחור שורה למחיקה", false)).Show(); return;
                }
                else
                {
                    line = selectedButton.Tag as LineDTO;
                    (sender as Button).Background = Brushes.LightGray;
                    selectedButton = null;
                }

            }

            bool b = LocationServise.instance.RemoveLine(line.Id);
            if (!b)
            {
                MessageBox.Show("ארעה שגיאה בהסרת השורה"); return;
            }
            selectedRow.Lines.Remove(line);
            SetRows();
            //remove last line
            //refresh rows view
        }
    }
}