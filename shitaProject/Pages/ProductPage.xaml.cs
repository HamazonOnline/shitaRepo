﻿using DTO;
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
    /// Interaction logic for ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        public ProductPage(ProductDTO p)
        {
            Initialize();
        }

        public ProductPage()
        {
            Initialize();
        }

        public void Initialize()
        {
            InitializeComponent();
            unitCB.ItemsSource = Enum.GetValues(typeof(CurrentData.UnitNames));
        }

        private void SearchProduct_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
