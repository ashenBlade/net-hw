FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FurAniJoGa.Domain/FurAniJoGa.Domain.csproj", "FurAniJoGa.Domain/"]
COPY ["FurAniJoGa.Infrastructure/FurAniJoGa.Infrastructure.csproj", "FurAniJoGa.Infrastructure/"]
COPY ["FurAniJoGa.WebHost.RabbitMqListener/FurAniJoGa.WebHost.RabbitMqListener.csproj", "FurAniJoGa.WebHost.RabbitMqListener/"]
RUN dotnet restore "FurAniJoGa.WebHost.RabbitMqListener/FurAniJoGa.WebHost.RabbitMqListener.csproj"
COPY . .
WORKDIR "/src/FurAniJoGa.WebHost.RabbitMqListener"
RUN dotnet build "FurAniJoGa.WebHost.RabbitMqListener.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FurAniJoGa.WebHost.RabbitMqListener.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FurAniJoGa.WebHost.RabbitMqListener.dll"]