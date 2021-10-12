module HW5.Parser
open System
open HW5.BinaryExpression
open HW5.Result

let TryParseOperand (operand: string): Result<int> =
    match Int32.TryParse operand with
    | true, i -> Success i
    | _ -> Failure

let TryParseOperands (left: string) (right: string): Result<int * int> =
    match TryParseOperand left with
    | Success l -> match TryParseOperand right with
                   | Success r -> Success(l, r)
                   | Failure -> Failure
    | Failure -> Failure

let supportedOperations = ["+"; "-"; "*"; "/"]

let TryParseOperation (op: string): Result<string> =
    match List.contains op supportedOperations with
    | true -> Success op
    | _ -> Failure

type TryParseBuilder() =
    member this.Bind(x, f) =
        match x with
        | Success s -> f s
        | Failure -> Failure

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
