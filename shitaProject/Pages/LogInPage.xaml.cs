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
using BLL;

namespace shitaProject.Pages
{
    /// <summary>
    /// Interaction logic for LogInPage.xaml
    /// </summary>
    public partial class LogInPage : Page
    {
        public LogInPage()
        {
            InitializeComponent();
            LocationCb.ItemsSource = BLL.LocationServise.instance.GetAllLocations();
        }
        private void Login(object sender, RoutedEventArgs e)
        {
            //check all data is field
            if (string.IsNullOrEmpty(UserNameTB.Text) || string.IsNullOrEmpty(PasswordTB.Text) || LocationCb.SelectedItem == null)
            {
                errorLbl.Content = "חלק מהנתונים חסרים";
                return;
            }
            //get current user and set in currentData.currentUser
            SimpleDTO user = BLL.LocationServise.instance.GetUserId(UserNameTB.Text, PasswordTB.Text);

            //if user not exist
            if (user == null)
            {
                errorLbl.Content = "שם משתמש או סיסמא לא נכונים";
                return;
            }
            CurrentData.instance.currentUser = user;
            //get current location and set in currentData.currentLocation
            CurrentData.instance.currentLocation = (SimpleDTO)LocationCb.SelectedItem;
            //move to main page
            ((MainWindow)Application.Current.MainWindow).PagesFrame.Content = null;
          ((MainWindow)Application.Current.MainWindow).myMenu.Visibility = Visibility.Visible;
            ((MainWindow)Application.Current.MainWindow).UserNameMenuItem.Header = CurrentData.instance.currentUser.Name;
            ((MainWindow)Application.Current.MainWindow).LogoImg.Height = 100;

        }

        private void UserNameTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            errorLbl.Content = "";
        }

        private void LocationCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            errorLbl.Content = "";
        }
    }
}
