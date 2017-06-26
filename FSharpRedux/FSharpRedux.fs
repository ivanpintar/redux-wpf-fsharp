namespace FSharpRedux

open System
open Taiste.Redux

module Actions = 
    let private dispatch action : ThunkAction<AppState> =
        let newAction (dispatcher:Redux.Dispatcher) getState = 
            let success = async {
                let! result = 
                    match action with
                    | AddTodo (id, text) -> DataAccess.addTodo id text
                    | ToggleTodo (id, status) -> DataAccess.changeTodoStatus id status
                    | DeleteTodo id -> DataAccess.deleteTodo id
                    | _ -> async { return true }

                if (result = true)
                then dispatcher.Invoke(action) |> ignore
                else ()
            } 
            Async.Start(success)

        new ThunkAction<AppState>(new Action<Redux.Dispatcher, Func<AppState>>(newAction))

    let addTodo text = AddTodo(Guid.NewGuid(), text) |> dispatch
    let toggleTodo id status = ToggleTodo(id, status) |> dispatch
    let deleteTodo id = DeleteTodo id |> dispatch
    let setFilter filter =  FilterTodos filter
    

module Reducers = 
    let private toggleTodo id status todo =
        if (todo.Id = id)
        then { todo with Status = status }
        else todo

    let private reduceApp appState action =
        let newTodos =
            match action with
            | ToggleTodo (id, status) -> appState.Todos |> List.map (toggleTodo id status)
            | AddTodo (id, text) -> appState.Todos @ [ { Id = id; Text = text; Status = Status.WIP } ]
            | DeleteTodo id -> appState.Todos |> List.filter (fun t -> t.Id <> id)
            | _ -> appState.Todos
    
        let newFilter =
            match action with
            | FilterTodos f -> f
            | _ -> appState.Filter
    
        { Todos = newTodos; Filter = newFilter }

    let reduce previousState (action:Redux.IAction) =
        match action with
        | :? FSharpRedux.Action as a -> reduceApp previousState a
        | _ -> previousState