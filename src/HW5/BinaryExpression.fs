module HW5.BinaryExpression

type BinaryExpression = { Left: decimal
                          Operation: (decimal -> decimal -> decimal)
                          Right: decimal }
let create left operation right = { Left = left
                                    Operation = operation
                                    Right = right }

let left expr = expr.Left
let operation expr = expr.Operation
let right expr = expr.Right

