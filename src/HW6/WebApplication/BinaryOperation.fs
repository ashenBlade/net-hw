module WebApplication.BinaryOperation


type BinaryOperation = {
    Operation: int * int -> Result<int, string>
    Operators: string list
}

let createOperation (func: int * int -> Result<int, string>) (operators: string list) =
    {
        Operation = func
        Operators = operators
    }

let add: BinaryOperation = createOperation (fun (a, b) -> Ok (a + b))
                               ["+"
                                "add"
                                "sum"
                                "summarize"]

let sub: BinaryOperation = createOperation (fun (a, b) -> Ok (a - b))
                               ["-"
                                "sub"
                                "subtract"]

let mul: BinaryOperation = createOperation (fun (a, b) -> Ok (a * b))
                               ["*"
                                "mul"
                                "multiply"]

let div: BinaryOperation = createOperation (fun (a, b) ->
                                                 match b with
                                                 | 0 -> Error "Can not divide by zero"
                                                 | _ -> Ok (a / b))
                               ["/"
                                "div"
                                "divide"]

let supportedOperations: BinaryOperation list = [ add
                                                  sub
                                                  mul
                                                  div ]

let operationNotSupportedErrorMessage operation =
    $"Operation {operation} is not supported!";

let tryParseOperation (opString: string): Result<BinaryOperation, string> =
    match List.tryFind (fun expr -> (List.contains opString expr.Operators)) supportedOperations with
    | Some expr-> Ok expr
    | _ -> Error (operationNotSupportedErrorMessage opString)