FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FurAniJoGa.WebHost.FileAPI.MetadataUpdater/FurAniJoGa.WebHost.FileAPI.MetadataUpdater.csproj", "FurAniJoGa.WebHost.FileAPI.MetadataUpdater/"]
RUN dotnet restore "FurAniJoGa.WebHost.FileAPI.MetadataUpdater/FurAniJoGa.WebHost.FileAPI.MetadataUpdater.csproj"
COPY . .
WORKDIR "/src/FurAniJoGa.WebHost.FileAPI.MetadataUpdater"
RUN dotnet build "FurAniJoGa.WebHost.FileAPI.MetadataUpdater.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FurAniJoGa.WebHost.FileAPI.MetadataUpdater.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FurAniJoGa.WebHost.FileAPI.MetadataUpdater.dll"]
