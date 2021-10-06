namespace HW3

module Parser =
    type ParsingResult =
        | Error of code: int
        | Success of Parser.ExpressionArguments
