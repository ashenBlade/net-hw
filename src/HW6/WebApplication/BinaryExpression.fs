module WebApplication.BinaryExpression

open BinaryOperation
open WebApplication

type BinaryExpression = { Left: int
                          Operation: BinaryOperation
                          Right: int }

let left expr = expr.Left
let right expr = expr.Right
let operation expr = expr.Operation

let createBinaryExpression left operation right = { Left = left
                                                    Operation = operation
                                                    Right = right }