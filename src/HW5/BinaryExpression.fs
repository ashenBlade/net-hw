module HW5.BinaryExpression

open System

type Operation =
    | Add
    | Sub
    | Div
    | Mul
    | Unknown

type BinaryExpression = { Left: decimal
                          Operation: Operation
                          Right: decimal }
let inline create left operation right = { Left = left
                                           Operation = operation
                                           Right = right }

let left expr = expr.Left
let operation expr = expr.Operation
let right expr = expr.Right

