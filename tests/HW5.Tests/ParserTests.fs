module HW5.Tests.ParserTests

open System
open HW5
open Xunit
open HW5.BinaryExpression
open HW5.Result
open AssertHelper

let assertOperationsEqual (expected) (actual) =
    Assert.Equal(operation expected, operation actual)

let assertOperandsEqual (expected: BinaryExpression) (actual: BinaryExpression): unit =
    Assert.Equal (left expected, left actual)
    Assert.Equal (right expected, right actual)
    Assert.Equal (operation expected, operation actual)

let assertExpressionsEqual expected actual =
    assertOperandsEqual expected actual

let getOperation (opString: string): Operation =
    match opString with
    | "+" -> Operation.Add
    | "-" -> Operation.Sub
    | "*" -> Operation.Mul
    | "/" -> Operation.Div
    | _ -> failwith "Not supported operation"

[<Theory>]
[<InlineData("1", "+", "1", 1, 1)>]
[<InlineData("123", "-", "190", 123, 190)>]
[<InlineData("0", "*", "111", 0, 111)>]
[<InlineData("323", "/", "456", 323, 456)>]
[<InlineData("5455", "+", "1", 5455, 1)>]
let ``TryParseArguments. With integers. Should return valid.`` left op right expectedLeft expectedRight =
    let actual = Parser.TryParseArguments [|left; op; right|]
    let expected = { Left = expectedLeft; Operation = (getOperation op); Right = expectedRight }
    match actual with
    | Success s -> assertExpressionsEqual expected s
    | Failure _ -> assertFail()

[<Theory>]
[<InlineData("1.23", "+", "1.01", 1.23, 1.01)>]
[<InlineData("321.22", "-", "190.1", 321.22, 190.1)>]
[<InlineData("0.9", "*", "11.12345", 0.9, 11.12345)>]
[<InlineData("323.23", "/", "456.7", 323.23, 456.7)>]
[<InlineData("595.44", "+", "1.23", 595.44, 1.23)>]
let ``TryParseArguments. With floating point. Should return valid.`` left op right expectedLeft expectedRight =
    let actual = Parser.TryParseArguments [|left; op; right|]
    let expected = { Left = expectedLeft; Operation = (getOperation op); Right = expectedRight }
    match actual with
    | Success s -> assertExpressionsEqual expected s
    | Failure _ -> assertFail()


[<Theory>]
[<InlineData("q", "+", "123")>]
[<InlineData("o", "+", "123")>]
[<InlineData("O", "+", "123")>]
[<InlineData("-", "+", "123")>]
[<InlineData(".", "+", "123")>]
[<InlineData("e", "+", "123")>]
[<InlineData("pi", "+", "123")>]
let ``TryParseArguments. With invalid left operand. Should return failure with message.`` left op right =
    let actual = Parser.TryParseArguments [|left; op; right|]
    let expectedMessage = Parser.getOperandInvalidMessage left
    match actual with
    | Success s -> assertFail()
    | Failure msg -> Assert.Equal(expectedMessage, msg)

[<Theory>]
[<InlineData("456", "*", "exp")>]
[<InlineData("80.9", "+", "o")>]
[<InlineData("90", "/", "O")>]
[<InlineData("23", "-", "-")>]
[<InlineData("11", "+", ".")>]
[<InlineData("23", "*", "e")>]
[<InlineData("123", "+", "pi")>]
let ``TryParseArguments. With invalid right operand. Should return failure with message.`` left op right =
    let actual = Parser.TryParseArguments [|left; op; right|]
    let expectedMessage = Parser.getOperandInvalidMessage right
    match actual with
    | Success s -> assertFail()
    | Failure msg -> Assert.Equal(expectedMessage, msg)




[<Theory>]
[<InlineData("456", "x", "456")>]
[<InlineData("80.9", "^", "456")>]
[<InlineData("90", ".", "456")>]
[<InlineData("23", "%", "456")>]
[<InlineData("11", "!", "456")>]
[<InlineData("23", "", "456")>]
[<InlineData("123", "&", "456")>]
let ``TryParseArguments. With not supported operation. Should return failure with message.`` left op right =
    let actual = Parser.TryParseArguments [|left; op; right|]
    let expectedMessage = Parser.getOperationNotSupportedMessage op
    match actual with
    | Success s -> assertFail()
    | Failure msg -> Assert.Equal(expectedMessage, msg)