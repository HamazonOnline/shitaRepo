using BLL;
using DTO;
using shitaProject.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace shitaProject.Windows
{
    /// <summary>
    /// Interaction logic for SetLineDetailsWindow.xaml
    /// </summary>
    public partial class SetLineDetailsWindow : Window
    {
        int lineId;
        public SetLineDetailsWindow(string curentProductName, string positionRow, string positionLine, int _lineId)
        {
            InitializeComponent();
            lineId = _lineId;
            CurrentProductNaneLbl.Content = (curentProductName == "MaterialDesignThemes.Wpf.PackIcon" ? "" : curentProductName);
            PositionLbl.Content = $"מיקום: מדף: {positionRow} שורה:{positionLine}";
        }

        private void IsDigitOnly(object sender, TextCompositionEventArgs e)
        {
            Regex _regex = new Regex("^[0-9]+$");
            e.Handled = !_regex.IsMatch(e.Text);
        }

        private void SearchProducts_Click(object sender, RoutedEventArgs e)
        {
            List<ProductDTO> products = BLL.ProductService.instance.GetProductsBySearch(BarcodeTb.Text, ProductNameTb.Text);
            ProductsDG.ItemsSource = products;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            BarcodeTb.Text = "";
            ProductNameTb.Text = "";
            ProductsDG.ItemsSource = null;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsDG.SelectedItem == null)
            {
                (new AutoCloseMessageBox(3, "לא נבחר מוצר", false)).Show();// MessageBox.Show();
                return;
            }
            else
            {
                ProductDTO product = (ProductDTO)ProductsDG.SelectedItem;

                LocationServise.instance.SetProductInLine(product.Id, lineId);
                (((MainWindow)Application.Current.MainWindow).PagesFrame.Content as RowsPage).GetRows();
                (((MainWindow)Application.Current.MainWindow).PagesFrame.Content as RowsPage).SetRows();
                this.Close();
            }
        }

        private void earse_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}