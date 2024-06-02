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

namespace shitaProject
{
    /// <summary>
    /// Interaction logic for ColumnView.xaml
    /// </summary>
    public partial class ColumnView : UserControl
    {
        public ColumnView(ColumsRealTimePage c ,string columnName)
        {
            InitializeComponent();
            DoorNameLbl.Content = columnName;
            this.DataContext = c;
        }
    }
}
