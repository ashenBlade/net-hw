module CalculatorTests

open HW3
open Xunit

[<Theory>]
[<InlineData(1, "+", 2, 3)>]
[<InlineData(1, "*", 2, 2)>]
[<InlineData(90, "-", 2, 88)>]
[<InlineData(90, "/", 2, 45)>]
[<InlineData(80, "+", 12343, 12423)>]
let ``Calculate. With valid arguments. Should calculate right`` (left: int) (operation: string) (right: int) (expected: int) =
    let actual = Calculator.Calculate left operation right
    Assert.Equal(expected, actual)

[<Theory>]
[<InlineData(12, "x", 23)>]
[<InlineData(12, "=", 23)>]
[<InlineData(12, "&", 23)>]
[<InlineData(12, "..", 23)>]
[<InlineData(12, "!", 0)>]
[<InlineData(12, "", 23)>]
[<InlineData(12, "", 23)>]
[<InlineData(12, "$", 23)>]
let ``Calculate. With invalid operation. Should return 0`` (left: int) (operation: string) (right: int) =
    let actual = Calculator.Calculate left operation right
    Assert.Equal(0, actual)

