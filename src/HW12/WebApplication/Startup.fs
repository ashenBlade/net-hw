module WebApplication.StartUp

open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Giraffe
open WebApplication.Calculator

let webApp = choose [ calculatorHttpHandler ]

type StartUp() =
    class
        member this.ConfigureServices(services: IServiceCollection) =
            services.AddGiraffe() |> ignore

        member this.Configure(app: IApplicationBuilder) =
            app.UseGiraffe webApp
    end

//let configureApp (app: IApplicationBuilder) =
//    // Add Giraffe to the ASP.NET Core pipeline
//    app.UseGiraffe webApp
//
//let configureServices (services: IServiceCollection) =
//    // Add Giraffe dependencies
//    services.AddGiraffe() |> ignore
//
//[<EntryPoint>]
//let main _ =
//    Host.CreateDefaultBuilder()
//        .ConfigureWebHostDefaults(
//            fun webHostBuilder ->
//                webHostBuilder
//                    .Configure(configureApp)
//                    .ConfigureServices(configureServices)
//                    |> ignore)
//        .Build()
//        .Run()
//    0
