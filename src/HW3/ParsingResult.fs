namespace HW3

type ParsingResult =
    | Error of code: int
    | Success