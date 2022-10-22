FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["FurAniJoGa.Domain/FurAniJoGa.Domain.csproj", "FurAniJoGa.Domain/"]
COPY "FurAniJoGa.RabbitMq.Contracts/FurAniJoGa.RabbitMq.Contracts.csproj" "FurAniJoGa.RabbitMq.Contracts/"
COPY ["FurAniJoGa.Infrastructure/FurAniJoGa.Infrastructure.csproj", "FurAniJoGa.Infrastructure/"]
COPY ["FurAniJoGa.WebHost.MessagesAPI.RabbitMqListener/FurAniJoGa.WebHost.MessagesAPI.RabbitMqListener.csproj", "FurAniJoGa.WebHost.MessagesAPI.RabbitMqListener/"]

RUN dotnet restore "FurAniJoGa.WebHost.MessagesAPI.RabbitMqListener/FurAniJoGa.WebHost.MessagesAPI.RabbitMqListener.csproj"

COPY "FurAniJoGa.Domain/" "FurAniJoGa.Domain/"
COPY "FurAniJoGa.Infrastructure/" "FurAniJoGa.Infrastructure/"
COPY "FurAniJoGa.RabbitMq.Contracts/" "FurAniJoGa.RabbitMq.Contracts/"
COPY 'FurAniJoGa.WebHost.MessagesAPI.RabbitMqListener/' 'FurAniJoGa.WebHost.MessagesAPI.RabbitMqListener/'


WORKDIR "/src/FurAniJoGa.WebHost.MessagesAPI.RabbitMqListener"
RUN dotnet build "FurAniJoGa.WebHost.MessagesAPI.RabbitMqListener.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FurAniJoGa.WebHost.MessagesAPI.RabbitMqListener.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FurAniJoGa.WebHost.MessagesAPI.RabbitMqListener.dll"]