using System.Windows;
using System.Windows.Controls;
using static FSharpRedux;

namespace ReduxWPF.Views
{
    /// <summary>
    /// Interaction logic for TodoItem.xaml
    /// </summary>
    public partial class TodoItem : UserControl
    {
        public static readonly DependencyProperty TodoProperty =
             DependencyProperty.Register("Todo", typeof(Todo), typeof(TodoItem), new PropertyMetadata(default(Todo), OnTodoChanged));

        public Todo Todo
        {
            get { return (Todo)GetValue(TodoProperty); }
            set { SetValue(TodoProperty, value); }
        }

        private static void OnTodoChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            var todoItem = (TodoItem)sender;
            var todo = (Todo)args.NewValue;

            todoItem.TodoItemTextBlock.Text = todo.Text;
            todoItem.CompleteCheckBox.IsChecked = todo.Status == Status.DONE;
            todoItem.DeleteTodoItemButton.Visibility = todo.Status != Status.DONE ? Visibility.Hidden : Visibility.Visible;
        }

        public TodoItem()
        {
            InitializeComponent();
        }

        private void CompleteCheckBox_Click(object sender, RoutedEventArgs e)
        {
            App.Store.Dispatch(Action.NewToggleTodo(Todo.Id, Status.DONE));
        }

        private void DeleteTodoItemButton_Click(object sender, RoutedEventArgs e)
        {
            App.Store.Dispatch(Action.NewDeleteTodo(Todo.Id));
        }
    }
}
