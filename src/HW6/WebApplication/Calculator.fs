module WebApplication.Calculator
open Giraffe

[<CLIMutable>]
type Values = { V1: int
                V2: int }
    let someHttpHandler: HttpHandler =
        fun next ctx ->
//            ctx.Response.StatusCode <- 500
            let values = ctx.TryBindQueryString<Values>()
            match values with
            | Ok v ->
                (setStatusCode 200 >=> json (v.V1 + v.V2)) next ctx
            | Error e ->
                (setStatusCode 400 >=> json e) next ctx

