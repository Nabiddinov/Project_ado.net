using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Xaml;
using Project_ado.net.DAL;
using Project_ado.net.Models;
using Project_ado.net.Modules;

namespace ExpenseManager_UI_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Category> category = new List<Category>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnIncomes_Click(object sender, RoutedEventArgs e)
        {
         //  category = CategoryService
        }
    }
}
