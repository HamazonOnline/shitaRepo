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
using System.Windows.Threading;

namespace shitaProject.Windows
{
    /// <summary>
    /// Interaction logic for AutoCloseMessageBox.xaml
    /// </summary>
    public partial class AutoCloseMessageBox : Window
    {
        int seconds;
        public AutoCloseMessageBox(int _seconds, string text, bool V)
        {
            InitializeComponent();
            if (!V)
            {
                iconPckIcn.Kind = MaterialDesignThemes.Wpf.PackIconKind.Warning;
                ColorElps.Fill = new SolidColorBrush(Colors.Red);
            }
            seconds = _seconds;
            myMessage.Content = text;
            //CancleButton.Focus();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer t = new DispatcherTimer()
            {
                Interval = new TimeSpan(0, 0, seconds),
            };
            t.Tick += CloseMe;
            t.Start();
        }

        private void CloseMe(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                this.Close();
            }), null);
        }

        private void CancleClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}