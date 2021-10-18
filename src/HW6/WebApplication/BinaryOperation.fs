module WebApplication.BinaryOperation


type BinaryOperation = decimal * decimal -> Result<decimal, string>

let add: BinaryOperation = fun (a, b) -> Ok (a + b)

let sub: BinaryOperation = fun (a, b) -> Ok (a - b)

let mul: BinaryOperation = fun (a, b) -> Ok (a * b)

let div: BinaryOperation = fun (a, b) ->
    match b with
    | 0m -> Error "Can not divide by zero"
    | _ -> Ok (a / b)