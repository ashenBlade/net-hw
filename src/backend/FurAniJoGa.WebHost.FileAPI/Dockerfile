FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FurAniJoGa.WebHost.FileAPI/FurAniJoGa.WebHost.FileAPI.csproj", "FurAniJoGa.WebHost.FileAPI/"]
COPY 'FurAniJoGa.RabbitMq.Contracts/FurAniJoGa.RabbitMq.Contracts.csproj' 'FurAniJoGa.RabbitMq.Contracts/'
COPY ["FurAniJoGa.FileAPI.Abstractions/FurAniJoGa.FileAPI.Abstractions.csproj", "FurAniJoGa.FileAPI.Abstractions/"]
COPY ["FurAniJoGa.FileAPI.Utility/FurAniJoGa.FileAPI.Utility.csproj", "FurAniJoGa.FileAPI.Utility/"]
RUN dotnet restore "FurAniJoGa.WebHost.FileAPI/FurAniJoGa.WebHost.FileAPI.csproj"

COPY ["FurAniJoGa.WebHost.FileAPI/", "FurAniJoGa.WebHost.FileAPI/"]
COPY 'FurAniJoGa.RabbitMq.Contracts/' 'FurAniJoGa.RabbitMq.Contracts/'
COPY ["FurAniJoGa.FileAPI.Abstractions/", "FurAniJoGa.FileAPI.Abstractions/"]
COPY ["FurAniJoGa.FileAPI.Utility/", "FurAniJoGa.FileAPI.Utility/"]

WORKDIR "/src/FurAniJoGa.WebHost.FileAPI"
RUN dotnet build "FurAniJoGa.WebHost.FileAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FurAniJoGa.WebHost.FileAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FurAniJoGa.WebHost.FileAPI.dll"]
