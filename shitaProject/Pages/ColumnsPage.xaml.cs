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
    /// Interaction logic for ColumnsPage.xaml
    /// </summary>
    public partial class ColumnsPage : Page
    {
        public ColumnsPage()
        {
            InitializeComponent();
            SetColumns();
        }

        public void SetColumns()
        {
            List<ColumnDTO> Columns = BLL.LocationServise.instance.GetColumnsPerLocation(CurrentData.instance.currentLocation.Id).OrderBy(x => x.LocationName).ToList();
            Button b;
            foreach (var item in Columns)
            {
                b = new Button();
                b.Content = item.Name;
                b.Tag = item;
                b.Click += GoToRowPage;
                ColumnsSp.Children.Add(b);
            }
        }

        private void GoToRowPage(object sender, RoutedEventArgs e)
        {
            ColumnDTO column = ((Button)sender).Tag as ColumnDTO;
            //if(column == 0)
            //{
            //    //add new column
            //}
            ((MainWindow)Application.Current.MainWindow).PagesFrame.Content = new RowsPage(column);
        }
    }
}
