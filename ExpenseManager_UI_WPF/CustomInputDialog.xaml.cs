using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseManager_UI_WPF;
using Project_ado.net.DAL;
using Project_ado.net.Models;
using System.Windows.Controls;
using System.Windows;

namespace ExpenseManager_UI_WPF
{
    public partial class CustomInputDialog : UserControl
    {
        public string Title { get; set; }
        public string InputValue => inputTextBox.Text;

        public CustomInputDialog(string title)
        {
            InitializeComponent();
            Title = title;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            // Close the dialog when the OK button is clicked
            var parentWindow = Window.GetWindow(this);
            parentWindow.DialogResult = true;
        }
    }
}
