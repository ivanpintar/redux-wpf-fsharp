using System;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using static FSharpRedux;

namespace ReduxWPF.Views
{
    /// <summary>
    /// Interaction logic for Footer.xaml
    /// </summary>
    public partial class Footer : UserControl
    {
        public Footer()
        {
            InitializeComponent();

            App.Store
                .Select(Selectors.MakeFooterViewModel)
                .Subscribe(vm =>
                {
                    ActiveTodoCounterTextBlock.Text = vm.ActiveTodosCounterMessage;
                    CheckFilter(vm.SelectedFilter);
                });

        }

        private void CheckFilter(TodoFilter selectedFilter)
        {
            if (selectedFilter == TodoFilter.DONE)
                CompletedFilter.IsChecked = true;
            else if (selectedFilter == TodoFilter.WIP)
                InProgressFilter.IsChecked = true;
            else
                AllFilter.IsChecked = true;
        }

        private void AllFilter_Click(object sender, RoutedEventArgs e)
        {
            FilterTodos(TodoFilter.ALL);
        }

        private void InProgressFilter_Click(object sender, RoutedEventArgs e)
        {
            FilterTodos(TodoFilter.WIP);
        }

        private void CompletedFilter_Click(object sender, RoutedEventArgs e)
        {
            FilterTodos(TodoFilter.DONE);
        }

        private void FilterTodos(TodoFilter filter)
        {
            App.Store.Dispatch(FSharpRedux.Action.NewFilterTodos(filter));
        }

    }
}
