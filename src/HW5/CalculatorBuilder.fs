module HW5.CalculatorBuilder
open HW5
open Result
open BinaryExpression

type CalculatorBuilder() =
    member this.Bind(x, f) =
           match x with
           | Success x -> f x
           | Failure msg -> Failure msg
    member this.Return(x) = Success x
    member this.ReturnFrom(x: BinaryExpression) = Success (Calculator.Calculate x)

let calculate = CalculatorBuilder()