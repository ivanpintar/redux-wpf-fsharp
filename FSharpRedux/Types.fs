namespace FSharpRedux

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
    | AddTodo of Guid * string
    | DeleteTodo of Guid
    | FilterTodos of TodoFilter
    interface Redux.IAction