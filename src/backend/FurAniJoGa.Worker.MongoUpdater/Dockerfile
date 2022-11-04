FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FurAniJoGa.Worker.MongoUpdater/FurAniJoGa.Worker.MongoUpdater.csproj", "FurAniJoGa.Worker.MongoUpdater/"]
RUN dotnet restore "FurAniJoGa.Worker.MongoUpdater/FurAniJoGa.Worker.MongoUpdater.csproj"
COPY . .
WORKDIR "/src/FurAniJoGa.Worker.MongoUpdater"
RUN dotnet build "FurAniJoGa.Worker.MongoUpdater.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FurAniJoGa.Worker.MongoUpdater.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FurAniJoGa.Worker.MongoUpdater.dll"]
