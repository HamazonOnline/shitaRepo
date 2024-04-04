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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace shitaProject.Pages
{
    /// <summary>
    /// Interaction logic for ColumnPage.xaml
    /// </summary>
    public partial class ColumnPage : Page
    {
        List<List<string>> lsts = new List<List<string>>();
        
        public ColumnPage()
        {
            InitializeComponent();
            ColumnCB.ItemsSource = ProductService.instance.GetColumnsPerLocation(1);

            for (int i = 0; i < 8; i++)
            {
                lsts.Add(new List<string>());
                for (int j = 0; j < 8; j++)
                {
                    lsts[i].Add("");
                }
            }
            //SelectColumn();
        }

        public void SelectColumn()
        {

        }

        private void SelectColumn(object sender, SelectionChangedEventArgs e)
        {
            int id = (ColumnCB.SelectedItem as SimpleDTO).Id;
            List<ProductLocationDTO> p = ProductService.instance.GetProductPerColumn(id);

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    lsts[i][j] = "";
                }
            }


            foreach (var item in p)
            {
                int row = int.Parse(item.RowName);
                int line = int.Parse(item.LineName);
                lsts[row][line] = item.Name;
            }
            lst.ItemsSource = null;
            lst.ItemsSource = lsts;
        }
    }
}
