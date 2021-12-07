module HW5.Calculator
open System
open System.Net.Sockets
open HW5.BinaryExpression
open Result

let divisionByZeroErrorMessage = "Division by zero is not allowed"
let operationUnknownErrorMessage = "Operation is unknown"
module Operations =
    let add (left: decimal) (right: decimal) = Success (left + right)
    let subtract (left: decimal) (right: decimal) = Success(left - right)
    let divide (left: decimal) (right: decimal) = match right with
                                                    | 0m -> Failure divisionByZeroErrorMessage
                                                    | _ -> Success(left / right)
    let multiply (left: decimal) (right: decimal) = Success (left * right)
    let unknown _ _ = Failure operationUnknownErrorMessage

let supportedOperationsStrings = dict [Operation.Add, Operations.add
                                       Operation.Sub, Operations.subtract
                                       Operation.Div, Operations.divide
                                       Operation.Mul, Operations.multiply
                                       Operation.Unknown, Operations.unknown]

let Calculate (args: BinaryExpression): Result<decimal> =
    supportedOperationsStrings.[operation args] (left args) (right args)