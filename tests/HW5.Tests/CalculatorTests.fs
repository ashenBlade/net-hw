module CalculatorTests

open System
open HW5
open HW5.Tests.AssertHelper
open HW5.BinaryExpression
open Xunit


[<Theory>]
[<InlineData(1, "+", 2, 3)>]
[<InlineData(1, "+", 2, 3)>]
[<InlineData(1, "+", 2, 3)>]
[<InlineData(1, "+", 2, 3)>]
let ``Calculate. With valid values. Should calculate right`` left operation right expected =
    let actual = Calculator.Calculate (create left operation right)
    ass expected actual