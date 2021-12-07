namespace HW3

open System

module Parser =
    type ExpressionArguments =
        { Left: int
          Operation: string
          Right: int }

    type ParsingResult =
        | Success of ExpressionArguments
        | Failure of code: int

    type private ParsedOperands = { Left: int; Right: int }
    type private UnparsedOperands = { Left: string; Right: string }
    type private ParsedOperation = string
    type private UnparsedOperation = string

    type private OperandParseResult =
        | Success of operands: ParsedOperands
        | Failure of code: int

    type private OperationParseResult =
        | Success of operation: ParsedOperation
        | Failure of code: int

    let supportedOperations: string list = [ "+"; "-"; "/"; "*" ]
    let operationNotSupportedErrorCode = 2
    let operandsInvalidErrorCode = 1

    let isOperationSupported (operation: string) : bool =
        List.contains operation supportedOperations

    let private parseOperation (str: UnparsedOperation) : OperationParseResult =
        match isOperationSupported str with
        | true -> OperationParseResult.Success str
        | _ -> OperationParseResult.Failure operationNotSupportedErrorCode



    let private parseOperands (str: UnparsedOperands) : OperandParseResult =
        match (Int32.TryParse str.Left) with
        | (true, op1) ->
            match (Int32.TryParse str.Right) with
            | (true, op2) -> OperandParseResult.Success { Left = op1; Right = op2 }
            | _ -> OperandParseResult.Failure operandsInvalidErrorCode
        | _ -> OperandParseResult.Failure operandsInvalidErrorCode

    let private parseBinaryExpression (operands: UnparsedOperands) (operation: UnparsedOperation) : ParsingResult =
        let operandsResult = parseOperands operands
        let operationResult = parseOperation operation

        match operandsResult with
        | OperandParseResult.Success operands ->
            match operationResult with
            | OperationParseResult.Success operation ->
                ParsingResult.Success
                    { Left = operands.Left
                      Operation = operation
                      Right = operands.Right }
            | OperationParseResult.Failure code -> ParsingResult.Failure code

        | OperandParseResult.Failure code -> ParsingResult.Failure code

    let public TryParseArguments (args: string []) : ParsingResult =
        parseBinaryExpression { Left = args.[0]; Right = args.[2] } args.[1]
