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
using System.Windows.Threading;

namespace shitaProject
{
    /// <summary>
    /// Interaction logic for ColumsRealTimePage.xaml
    /// </summary>
    public partial class ColumsRealTimePage : Page
    {
        List<ColumnRealTimeDTO> Columns = new List<ColumnRealTimeDTO>();
        DispatcherTimer timer = new DispatcherTimer();

        public ColumsRealTimePage()
        {
            InitializeComponent();
            SetColumns();

            timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            List<SimpleDTO> columnsMode = BLL.LocationServise.instance.GetColumnsMode(CurrentData.instance.currentLocation.Id);
            foreach (var item in columnsMode)
            {
                ColumnRealTimeDTO c = Columns.First(x => x.Id == item.Id);
                if (c != null)
                    c.Locked = int.Parse(item.Name); //???
                ColumnsSp.UpdateLayout();
            }
        }

        public void SetColumns()
        {
            Columns = BLL.LocationServise.instance.GetColumnsPerLocationRT(CurrentData.instance.currentLocation.Id).OrderBy(x => x.LocationName).ToList();
            Label b;
            foreach (var item in Columns)
            {
                Binding myBinding = new Binding("Name");
                myBinding.Mode = BindingMode.TwoWay;
                //myBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                b = new Label();
                b.SetBinding(Label.ContentProperty, myBinding);
                
                b.DataContext = item;
                b.Content = new Binding("Name"); //item.Name;
                b.Tag = item;
                ColumnsSp.Children.Add(b);
                ColumnsSp.UpdateLayout();
            }
        }

        
    }
}
