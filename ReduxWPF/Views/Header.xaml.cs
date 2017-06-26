using System.Windows.Controls;
using System.Windows.Input;
using static FSharpRedux;

namespace ReduxWPF.Views
{
    /// <summary>
    /// Interaction logic for Header.xaml
    /// </summary>
    public partial class Header : UserControl
    {
        public Header()
        {
            InitializeComponent();
        }

        private void TodoInputTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
                return;

            App.Store.Dispatch(Action.NewAddTodo(TodoInputTextBox.Text));
            TodoInputTextBox.Text = string.Empty;
        }
    }
}
