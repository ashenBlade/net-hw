module HW.WebApplication

open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.DependencyInjection
open Giraffe
open WebApplication
open WebApplication.Calculator
open WebApplication.StartUp

//let webApp =
//    choose [
////        route "/ping"   >=> text "pong"
////        route "/add" >=> someHttpHandler
////        route "/" >=> calculatorHttpHandler
////        setStatusCode 404 >=> text "Not found!" ]
//          calculatorHttpHandler ]

//let configureApp (app : IApplicationBuilder) =
//    // Add Giraffe to the ASP.NET Core pipeline
//    app.UseGiraffe webApp
//
//let configureServices (services : IServiceCollection) =
//    // Add Giraffe dependencies
//    services.AddGiraffe() |> ignore

[<EntryPoint>]
let main _ =
    Host.CreateDefaultBuilder()
        .ConfigureWebHostDefaults(
            fun webHostBuilder ->
                webHostBuilder
//                    .Configure(configureApp)
//                    .ConfigureServices(configureServices)
                    .UseStartup<StartUp>()
                    |> ignore)

        .Build()
        .Run()
    0