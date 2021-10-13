module HW5.Program

open HW5.Parser
open HW5.Result

open CalculatorBuilder

[<EntryPoint>]
let main args =
    let result = calculate {
        let! expression = TryParseArguments args
        let! result = Calculator.Calculate expression
        return result
    }
    match result with
    | Success res -> printf $"Result: {res}"
    | Failure msg -> printf $"Error: {msg}"
    0