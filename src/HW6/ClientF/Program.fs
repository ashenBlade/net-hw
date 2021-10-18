// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open FSharp.Data

let doRequest =
    async {
        let! r1 = Http.AsyncRequest "http://localhost:5000/add?v1=1&v2=3"
        let str = match r1.Body with
                    | HttpResponseBody.Text t -> t
                    | HttpResponseBody.Binary _ -> "Binary"
        return str
    }

// Define a function to construct a message to print
let from whom =
    sprintf "from %s" whom

[<EntryPoint>]
let main argv =
    let message = doRequest |> Async.RunSynchronously
    printfn "%s" message
    0 // return an integer exit code