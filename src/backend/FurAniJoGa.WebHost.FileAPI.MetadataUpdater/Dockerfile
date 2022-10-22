FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["FurAniJoGa.WebHost.FileAPI.MetadataUpdater/FurAniJoGa.WebHost.FileAPI.MetadataUpdater.csproj", "FurAniJoGa.WebHost.FileAPI.MetadataUpdater/"]
COPY ["FurAniJoGa.FileAPI.Abstractions/FurAniJoGa.FileAPI.Abstractions.csproj", "FurAniJoGa.FileAPI.Abstractions/"]
COPY ["FurAniJoGa.FileAPI.Utility/FurAniJoGa.FileAPI.Utility.csproj", "FurAniJoGa.FileAPI.Utility/"]
COPY 'FurAniJoGa.RabbitMq.Contracts/FurAniJoGa.RabbitMq.Contracts.csproj' 'FurAniJoGa.RabbitMq.Contracts/'

RUN dotnet restore "FurAniJoGa.WebHost.FileAPI.MetadataUpdater/FurAniJoGa.WebHost.FileAPI.MetadataUpdater.csproj"

COPY 'FurAniJoGa.WebHost.FileAPI.MetadataUpdater/' 'FurAniJoGa.WebHost.FileAPI.MetadataUpdater/' 
COPY 'FurAniJoGa.RabbitMq.Contracts/' 'FurAniJoGa.RabbitMq.Contracts/'
COPY ["FurAniJoGa.FileAPI.Abstractions/", "FurAniJoGa.FileAPI.Abstractions/"]
COPY ["FurAniJoGa.FileAPI.Utility/", "FurAniJoGa.FileAPI.Utility/"]

WORKDIR "/src/FurAniJoGa.WebHost.FileAPI.MetadataUpdater"
RUN dotnet build "FurAniJoGa.WebHost.FileAPI.MetadataUpdater.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FurAniJoGa.WebHost.FileAPI.MetadataUpdater.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FurAniJoGa.WebHost.FileAPI.MetadataUpdater.dll"]
