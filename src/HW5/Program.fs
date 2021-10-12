module HW5.Program

open HW5.Parser
open HW5.Calculator
open HW5.Result

let (>=>) x f =
    match x with
    | Success s -> f s
    | Failure -> 0

[<EntryPoint>]
let main args =
    let res = TryParseArguments args >=> Calculator.Calculate
    printfn $"{res}"
    0