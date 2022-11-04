FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FurAniJoGa.WebHost.MetadataAPI/FurAniJoGa.WebHost.MetadataAPI.csproj", "FurAniJoGa.WebHost.MetadataAPI/"]
RUN dotnet restore "FurAniJoGa.WebHost.MetadataAPI/FurAniJoGa.WebHost.MetadataAPI.csproj"
COPY . .
WORKDIR "/src/FurAniJoGa.WebHost.MetadataAPI"
RUN dotnet build "FurAniJoGa.WebHost.MetadataAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FurAniJoGa.WebHost.MetadataAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FurAniJoGa.WebHost.MetadataAPI.dll"]
