module HW5.Parser
open System
open System.Runtime.InteropServices
open HW5.BinaryExpression
open HW5.Result

let getOperandInvalidMessage (operand: string) =
     $"Operand is invalid: {operand}"


let TryParseOperand (operand: string): Result<decimal> =
    match Decimal.TryParse operand with
    | true, i -> Success i
    | _ -> Failure (getOperandInvalidMessage operand)

let TryParseOperands (left: string) (right: string): Result<decimal * decimal> =
    match TryParseOperand left with
    | Success l -> match TryParseOperand right with
                   | Success r -> Success(l, r)
                   | Failure f -> Failure f
    | Failure f-> Failure f

let supportedOperations = dict ["+", (+)
                                "-", (-)
                                "/", (/)
                                "*", (*)]

let getOperationNotSupportedMessage (operation: string) =
    let supportedString = supportedOperations |> Seq.map (fun pair -> pair.Key)
                                              |> String.concat ", "
    $"Operation {operation} is not supported\n" +
    $"Supported operations are {supportedString}"

let TryParseOperation (op: string): Result<decimal -> decimal-> decimal> =
    match supportedOperations.TryGetValue op with
    | true, op -> Success op
    | _ -> Failure (getOperationNotSupportedMessage op)

type TryParseBuilder() =
    member this.Bind(x, f) =
        match x with
        | Success s -> f s
        | Failure f -> Failure f

    member this.Return(x) =
        Success x

let tryParse = TryParseBuilder()

let TryParseArguments (args: string[]): Result<BinaryExpression> =
    tryParse {
        let! operands = TryParseOperands args.[0] args.[2]
        let! operation = TryParseOperation args.[1]
        let (left, right) = operands.Deconstruct()
        return { Left = left
                 Operation = operation
                 Right = right }
    }
