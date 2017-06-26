using Microsoft.FSharp.Collections;
using Redux;
using System.Windows;
using FSharpRedux;
using Taiste.Redux;

namespace ReduxWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IStore<AppState> Store { get; private set; }

        public App()
        {
            InitializeComponent();

            var initialState = new AppState(FSharpList<Todo>.Empty, TodoFilter.ALL);

            Store = new Store<AppState>(Reducers.reduce, initialState, Middleware.ThunkMiddleware);
        }
    }
}
