using ReduxWPF.ViewModels;
using System.Collections.Generic;
using System.Linq;
using FSharpRedux;

namespace ReduxWPF
{
    public class Selectors
    {
        public static IEnumerable<Todo> GetFilteredTodos(AppState state)
        {
            if (state.Filter == TodoFilter.WIP)
                return state.Todos.Where(x => x.Status == Status.WIP);
            if (state.Filter == TodoFilter.DONE)
                return state.Todos.Where(x => x.Status == Status.DONE);

            return state.Todos;
        }

        public static FooterViewModel MakeFooterViewModel(AppState state)
        {
            return new FooterViewModel
            {
                ActiveTodosCounterMessage = GetActiveTodosCounterMessage(state.Todos),
                SelectedFilter = state.Filter,
                AreFiltersVisible = state.Todos.Any()
            };
        }

        public static string GetActiveTodosCounterMessage(IEnumerable<Todo> todos)
        {
            var activeTodoCount = todos.Count(todo => todo.Status != Status.DONE);
            var itemWord = activeTodoCount == 1 ? "item" : "items";
            return activeTodoCount + " " + itemWord + " left";
        }
    }
}
