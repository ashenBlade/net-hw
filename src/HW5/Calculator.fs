module HW5.Calculator
open HW5.BinaryExpression
open Result

let Calculate (args: BinaryExpression): Result<decimal> =
    match args.Operation, args.Right with
    | (/), 0m -> Failure "Dividing by zero is not allowed"
    | _ -> Success (args.Operation args.Left args.Right)