using static FSharpRedux;

namespace ReduxWPF.ViewModels
{
    public class FooterViewModel
    {
        public string ActiveTodosCounterMessage { get; internal set; }
        public bool AreFiltersVisible { get; internal set; }
        public TodoFilter SelectedFilter { get; internal set; }
    }
}