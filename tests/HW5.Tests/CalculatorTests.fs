module CalculatorTests

open System
open HW5
open HW5.Tests.AssertHelper
open HW5.BinaryExpression
open HW5.Result
open Xunit


[<Theory>]
[<InlineData(1, 2, 3)>]
[<InlineData(1, -1, 0)>]
[<InlineData(90909, 123, 91032)>]
[<InlineData(56, 11, 67)>]
let ``Calculate. With valid values and plus operation. Should calculate right`` left right (expected: decimal) =
    let actual = Calculator.Calculate (create left ((Operation.Add)) right)
    match actual with
    | Success s -> ass expected s
    | Failure _ -> Assert.True false

[<Theory>]
[<InlineData(1, 2, -1)>]
[<InlineData(1, -1, 2)>]
[<InlineData(90909.999, 123.123, 90786.876)>]
[<InlineData(-1.123, 1, -2.123)>]
let ``Calculate. With valid values and minus operation. Should calculate right`` left right (expected: decimal) =
    let actual = Calculator.Calculate (create left (Operation.Sub) right)
    match actual with
    | Success s -> ass expected s
    | Failure _ -> Assert.True false

[<Theory>]
[<InlineData(1, 2, 0.5)>]
[<InlineData(1, -1, -1)>]
[<InlineData(90909, 123, 739.097560976)>]
[<InlineData(81, 9, 9)>]
[<InlineData(81.9, 9, 9.1)>]
[<InlineData(90234, 9.67, 9331.334022751)>]
let ``Calculate. With valid values and division operation. Should calculate right`` left right expected =
    let actual = Calculator.Calculate (create left Operation.Div right)
    match actual with
    | Success s -> ass expected s
    | Failure _ -> Assert.True false

[<Theory>]
[<InlineData(1, 2, 2)>]
[<InlineData(1, -1, -1)>]
[<InlineData(90909, 123, 11181807)>]
[<InlineData(56, 11, 616)>]
[<InlineData(5.5344, 112320.3, 621625.46832)>]
let ``Calculate. With valid values and multiplying operation. Should calculate right`` left right (expected: decimal) =
    let actual = Calculator.Calculate (create left Operation.Mul right)
    match actual with
    | Success s -> ass expected s
    | Failure _ -> Assert.True false

[<Theory>]
[<InlineData(1, 0)>]
[<InlineData(0, 0)>]
[<InlineData(124.5643, 0)>]
[<InlineData(124.23, 0)>]
[<InlineData(98, 0)>]
let ``Calculate. With valid operand, division operation and 0 as left operand. Should calculate right`` left right =
    let actual = Calculator.Calculate (create left Operation.Div right)
    match actual with
    | Success s -> assertFail()
    | Failure msg -> Assert.Equal(msg, Calculator.divisionByZeroErrorMessage)