FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TicTacToe.RabbitListener/TicTacToe.RabbitListener.csproj", "TicTacToe.RabbitListener/"]
COPY ["TicTacToe.Events/TicTacToe.Events.csproj", "TicTacToe.Events/"]
RUN dotnet restore "TicTacToe.RabbitListener/TicTacToe.RabbitListener.csproj"
COPY . .
WORKDIR "/src/TicTacToe.RabbitListener"
RUN dotnet build "TicTacToe.RabbitListener.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TicTacToe.RabbitListener.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TicTacToe.RabbitListener.dll"]
