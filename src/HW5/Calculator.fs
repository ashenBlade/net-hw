module HW5.Calculator
open HW5.BinaryExpression

let Calculate (args: BinaryExpression) =
    match operation args with
    | "+" -> (left args) + (right args)
    | "-" -> (left args) + (right args)
    | "/"
          when (right args) <> 0 -> (left args) / (right args)
    | "*" -> (left args) * (right args)
    | _ -> 0