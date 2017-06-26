module FSharpRedux

open System

type Status =
    | WIP
    | DONE

type TodoFilter =
    | WIP
    | DONE
    | ALL

type Todo = { Id : Guid; Text : string; Status: Status }
type AppState = { Todos : Todo list; Filter: TodoFilter }

type Action =
    | ToggleTodo of Guid * Status
    | AddTodo of string
    | DeleteTodo of Guid
    | FilterTodos of TodoFilter
    interface Redux.IAction
    
let toggleTodo id status todo =
    if (todo.Id = id)
    then { todo with Status = status }
    else todo

let reduceApp appState action =
    let newTodos =
        match action with
        | ToggleTodo (id, status) -> appState.Todos |> List.map (toggleTodo id status)
        | AddTodo text -> appState.Todos @ [ { Id = Guid.NewGuid(); Text = text; Status = Status.WIP } ]
        | DeleteTodo id -> appState.Todos |> List.filter (fun t -> t.Id <> id)
        | _ -> appState.Todos
    
    let newFilter =
        match action with
        | FilterTodos f -> f
        | _ -> appState.Filter
    
    { Todos = newTodos; Filter = newFilter }

let reduce previousState (action:Redux.IAction) =
    match action with
    | :? Action as a -> reduceApp previousState a
    | _ -> previousState