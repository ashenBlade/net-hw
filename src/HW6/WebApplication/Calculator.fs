module WebApplication.Calculator
open System.Runtime.InteropServices
open Giraffe
open Microsoft.AspNetCore.Http
open WebApplication
open WebApplication.BinaryOperation
open BinaryExpression

[<CLIMutable>]
type RequestedValues = { V1: int
                         V2: int }

module RequestedValues =
    let tryParseInput (values: RequestedValues) (operation: string): Result<BinaryExpression, string> =
        let left = values.V1
        let right = values.V2
        let operationResult = tryParseOperation operation
        match operationResult with
        | Result.Ok operation -> Ok (createBinaryExpression left operation right)
        | Error msg -> Error msg



let calculate (expression: BinaryExpression): Result<int, string> =
    expression.Operation.Operation (expression.Left, expression.Right)

//let someHttpHandler: HttpHandler =
//        fun next ctx ->
////            ctx.Response.StatusCode <- 500
//            let values = ctx.TryBindQueryString<RequestedValues>()
//            match values with
//            | Ok v ->
//                match RequestedValues.tryParseInput v with
//                | Ok s ->( setStatusCode 200 >=> json (calculate s))
//                             next ctx
//                | Error msg -> (setStatusCode 400 >=> json msg)
//                                   next ctx
//
////                (setStatusCode 200 >=> json (v.V1 + v.V2)) next ctx
//            | Error e ->
//                (setStatusCode 400 >=> json (ctx.GetRequestUrl())) next ctx

let getRequestedOperationString (path: string): string =
    Array.last (path.Split '/')

let processError msg: HttpHandler =
    setStatusCode 400 >=> json msg

let calculatorHttpHandler: HttpHandler =
    fun next ctx ->
        let operationResult = tryParseOperation( getRequestedOperationString (ctx.Request.Path.Value))
        let valuesResult = ctx.TryBindQueryString<RequestedValues>()
        (match valuesResult with
        | Ok values ->
            match operationResult with
            | Ok operation ->
                let expression = createBinaryExpression values.V1 operation values.V2
                let result = calculate expression
                match result with
                | Ok result -> (setStatusCode 200 >=> json result)
                | Error msg -> processError msg
            | Error msg -> processError msg
        | Error msg -> processError msg ) next ctx
//        let expression = RequestedValues.tryParseInput values operation
//        (setStatusCode 400 >=> json (operation)) next ctx
