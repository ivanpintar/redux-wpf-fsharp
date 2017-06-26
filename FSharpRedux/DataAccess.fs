module FSharpRedux.DataAccess

open System.Threading.Tasks
open System

let addTodo id text = async {
    do! Async.Sleep 2000
    return true
    }
       

let changeTodoStatus id status = async {
    do! Async.Sleep 2000
    return true
    }

let deleteTodo id = async {
    do! Async.Sleep 2000
    return true
    }