module HW5.BinaryExpression

type BinaryExpression = { Left: int
                          Operation: string
                          Right: int }
let create left operation right = { Left = left
                                    Operation = operation
                                    Right = right }

let left expr = expr.Left
let operation expr = expr.Operation
let right expr = expr.Right

