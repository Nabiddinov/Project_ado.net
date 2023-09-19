using System.Windows.Controls;
using System.Windows;

namespace ExpenseManager_UI_WPF
{
    public partial class InputDialog : UserControl
    {
        public string InputValue { get; private set; }
       
        public InputDialog()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            InputValue = inputTextBox.Text;
            CloseDialog();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            InputValue = null;
            CloseDialog();
        }

        private void CloseDialog()
        {
            var parentWindow = Window.GetWindow(this) as Window;
            if (parentWindow != null)
            {
                parentWindow.Close();
            }
        }

       
    }

}
