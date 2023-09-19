using System.Windows;
using System.Windows.Controls;

namespace ExpenseManager_UI_WPF
{
    public partial class CustomInputDialog : UserControl
    {
        public string Title { get; set; }
        public string InputValue => customInputTextBox.Text;

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

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            // Close the dialog when the Cancel button is clicked
            var parentWindow = Window.GetWindow(this);
            parentWindow.DialogResult = false;
        }
    }
}
