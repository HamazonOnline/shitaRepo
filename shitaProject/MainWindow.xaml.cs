using shitaProject.Pages;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DTO;


namespace shitaProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string productsFileName = "";
        private static DateTime? compileDate;
        private static System.Reflection.Assembly executingAssembly;

        public MainWindow()
        {
            InitializeComponent();
            PagesFrame.Content = new LogInPage();
            myMenu.Visibility = Visibility.Collapsed;

            //ProductsPage.viewProductPage += viewProductPage;

            string version = GetPublishedVersion();//ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();// GetPublishedVersion();
            verNum.Content = version;

        }

        static readonly Regex trimmer = new Regex(@"\s\s+");

        private string GetPublishedVersion()
        {
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                string date;
                Version version = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
                //CurrentData.Global.Version = version.ToString();
                var retval = $"V{version.ToString()} , DATE : ";

                if (!compileDate.HasValue)
                    compileDate = Directory.GetCreationTime(ExecutingAssembly.Location);
                if (compileDate != null)
                    date = compileDate.Value.ToString("dd-MM-yy");
                else
                    date = new DateTime().ToString();
                retval += date;
                return retval;
            }
            return "Not network deployed";
        }

        public static System.Reflection.Assembly ExecutingAssembly
        {
            get { return executingAssembly ?? (executingAssembly = System.Reflection.Assembly.GetExecutingAssembly()); }
        }

        private void viewProductPage()
        {
            GoToNavigate("ניהול פריט");
           // PagesFrame.Content = new ProductCardPage();
        }


        private void GoToNavigate(object sender, RoutedEventArgs e)
        {
            //PagesFrame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Visible;
            GoToNavigate((sender as System.Windows.Controls.MenuItem).Header.ToString());
        }

        private void GoToNavigate(string header)
        {
            DateTime time = DateTime.Now;
            //PagesFrame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Visible;
            headerT.Text = header;
            hederShow.Visibility = Visibility.Visible;
            switch (header)
            {
                case "ניהול פריט": PagesFrame.Content = new ProductPage(); break;
                case "רשימת פריטים": PagesFrame.Content = new ProductsPage(); break;
                case "תצוגת מדף": PagesFrame.Content = new ColumnPage(); break;
                case "הגדרת מיקומים": PagesFrame.Content = new ColumnsPage(); break;
                //case "טרנסקציות": PagesFrame.Content = new TransactionPage(); break;
            }
        }

        private void PagesFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void GoToLogOut(object sender, RoutedEventArgs e)
        {
            PagesFrame.Content = new LogInPage();
            headerT.Text = "";
            myMenu.Visibility = Visibility.Hidden;
            LogoImg.Height = 150;
            CurrentData.instance.LogOut();
        }
    }
}