FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TicTacToe.Web/TicTacToe.Web.csproj", "TicTacToe.Web/"]
RUN dotnet restore "TicTacToe.Web/TicTacToe.Web.csproj"
COPY . .
WORKDIR "/src/TicTacToe.Web"
RUN dotnet build "TicTacToe.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TicTacToe.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TicTacToe.Web.dll"]
