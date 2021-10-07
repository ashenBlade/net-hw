namespace HW3

open System

// parseLeft(parseOperand(parseRight(right: string), operand: string), left) :

module Parser =
    type private ParsedOperands = { Left: int; Right: int }
    type private UnparsedOperands = { Left: string; Right: string }
    type private ParsedOperation = string
    type private UnparsedOperation = string

    type private OperandParseResult =
        | Success of operands: ParsedOperands
        | Fail of code: int

    type private OperationParseResult =
        | Success of operation: ParsedOperation
        | Fail of code: int

    let supportedOperations: string list = [ "+"
                                             "-"
                                             "/"
                                             "*" ]
    let operationNotSupportedErrorCode = 2
    let operandsInvalidErrorCode = 1
    let isOperationSupported (operation: string): bool = List.contains operation supportedOperations

    let private parseOperation (str: UnparsedOperation) : OperationParseResult =
        match isOperationSupported str with
        | true -> OperationParseResult.Success str
        | _ -> OperationParseResult.Fail operationNotSupportedErrorCode



    let private parseOperands (str: UnparsedOperands) : OperandParseResult =
        match (Int32.TryParse str.Left) with
        | (true, op1) -> match (Int32.TryParse str.Right) with
                                | (true, op2) -> OperandParseResult.Success { Left = op1
                                                                              Right = op2 }
                                | _ -> OperandParseResult.Fail operandsInvalidErrorCode
        | _ ->  OperandParseResult.Fail operandsInvalidErrorCode

    let private parseBinaryExpression (operands: UnparsedOperands) (operation: UnparsedOperation): Parser.ParsingResult =
        let operandsResult = parseOperands operands
        let operationResult = parseOperation operation
        match operandsResult with
        | OperandParseResult.Success operands -> match operationResult with
                                                 | OperationParseResult.Success operation -> Parser.ParsingResult.Success { Left = operands.Left
                                                                                                                            Operation = operation
                                                                                                                            Right = operands.Right }
                                                 | OperationParseResult.Fail code -> Parser.ParsingResult.Error code
        | OperandParseResult.Fail code -> Parser.ParsingResult.Error code

    let public TryParseArguments (args: string []) : Parser.ParsingResult =
        parseBinaryExpression { Left = args.[0]; Right = args.[2] } args.[1]
