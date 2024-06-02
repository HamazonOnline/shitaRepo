using BLL;
using DTO;
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

namespace shitaProject.Pages
{
    /// <summary>
    /// Interaction logic for ProductsOage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        public delegate void ViewProductPage();
        public static event ViewProductPage viewProductPage;
        List<ProductDTO> products;

        public ProductsPage(ProductDTO _product)
        {
            //dataContext = _product;
            initialize();
        }

        public ProductsPage()
        {
            initialize();
        }

        public void initialize()
        {
            InitializeComponent();
            products = ProductService.instance.GetProductsPerLocation(CurrentData.instance.currentLocation.Id);
            ProductsDG.ItemsSource = products;
            ProductsDG.CellEditEnding += new EventHandler<DataGridCellEditEndingEventArgs>(Price_EditEnding);
        }

        private void Price_EditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            ProductDTO p = (ProductDTO)ProductsDG.SelectedItem;
            if (p != null)
            {
                //set edited price in string s
                string s = ((TextBox)e.EditingElement).Text;
                //check if the price is a number
                if (!double.TryParse(s, out double result) || result < 0)
                {
                    System.Windows.MessageBox.Show("המחיר חייב להיות מספר שלם ");
                    ((TextBox)e.EditingElement).Text = p.Price.ToString();
                    return;
                }
                p.Price = result;
                ProductService.instance.UpdateProductPrice(p.Id, p.Price);
            }
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            //טבלה מכילה את רשימת המוצרים שיש להם מיקומים. לכל אחד מחיר- ניתן לעריכה ושינוי
            //מצב מלאי
            //הצגת משקל ברוטו. לא ניתן לעריכה
            ((MainWindow)Application.Current.MainWindow).PagesFrame.Content = new ProductPage(sender as ProductDTO);
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            int selectedproviderId = ProviderCB.SelectedValue != null ? (int)ProviderCB.SelectedItem : 0;
            ProductsDG.ItemsSource = products.Where(x => (string.IsNullOrEmpty(BarcodeTB.Text) == null ? true : (x.Barcode != null ? x.Barcode.EndsWith(BarcodeTB.Text) : false)));
            // && (selectedproviderId > 0 ? x.ProviderId == (int)ProviderCB.SelectedValue : true)).ToList(); ;
        }

        private void CancleFilter_Click(object sender, RoutedEventArgs e)
        {
            ProviderCB.SelectedItem = null;
            BrandCB.SelectedItem = null;
            BarcodeTB.Text = "";
            ProductsDG.ItemsSource = products;
        }
    }
}