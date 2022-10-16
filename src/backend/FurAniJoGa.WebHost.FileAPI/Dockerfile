FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FurAniJoGa.WebHost.FileAPI/FurAniJoGa.WebHost.FileAPI.csproj", "FurAniJoGa.WebHost.FileAPI/"]
RUN dotnet restore "FurAniJoGa.WebHost.FileAPI/FurAniJoGa.WebHost.FileAPI.csproj"
COPY . .
WORKDIR "/src/FurAniJoGa.WebHost.FileAPI"
RUN dotnet build "FurAniJoGa.WebHost.FileAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FurAniJoGa.WebHost.FileAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FurAniJoGa.WebHost.FileAPI.dll"]
