module ParserTests

open HW3.Parser
open Xunit
let fail = Assert.True(false)
let getExpressionArguments (left: int) (op: string) (right: int): ExpressionArguments =
    { Left = left; Operation = op; Right = right }

[<Theory>]
[<InlineData("80", "+", "90", 80, "+", 90)>]
[<InlineData("80", "*", "0", 80, "*", 0)>]
[<InlineData("9123", "-", "90", 9123, "-", 90)>]
[<InlineData("9254", "/", "-12", 9254, "/", -12)>]
[<InlineData("43", "-", "23", 43, "-", 23)>]
[<InlineData("80", "*", "342", 80, "*", 342)>]
[<InlineData("0", "+", "0", 0, "+", 0)>]
let ``TryParseArguments. With valid arguments. Should parse right`` (arg1: string) (arg2: string) (arg3: string)
                                                                    (expectedLeft: int) (expectedOperation: string) (expectedRight: int) =
    let expected = getExpressionArguments expectedLeft expectedOperation expectedRight
    match (TryParseArguments [|arg1; arg2; arg3|]) with
    | ParsingResult.Success actual -> Assert.Equal(expected, actual)

[<Theory>]
[<InlineData("", "+", "12")>]
[<InlineData("90", "/", "O")>]
[<InlineData("q", "+", "0")>]
[<InlineData("", "+", "p")>]
[<InlineData("2+3", "+", "123")>]
[<InlineData(".", "-", "00")>]
let ``TryParseArguments. With only invalid operands. Should return error code`` (arg1: string) (arg2: string) (arg3: string) =
    match (TryParseArguments [|arg1; arg2; arg3|]) with
    | ParsingResult.Failure code -> Assert.Equal(operandsInvalidErrorCode, code)


[<Theory>]
[<InlineData("90", "x", "80")>]
[<InlineData("90", ".", "80")>]
[<InlineData("90", "!", "80")>]
[<InlineData("90", "", "80")>]
[<InlineData("90", "&", "80")>]
[<InlineData("90", "^", "80")>]
let ``TryParseArguments. With only not supported operation. Should return error code`` (arg1: string) (arg2: string) (arg3: string) =
    match (TryParseArguments [|arg1; arg2; arg3|]) with
    | ParsingResult.Failure code -> Assert.Equal (operationNotSupportedErrorCode, code)