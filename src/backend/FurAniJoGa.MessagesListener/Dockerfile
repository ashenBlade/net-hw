FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FurAniJoGa.MessagesListener/FurAniJoGa.MessagesListener.csproj", "FurAniJoGa.MessagesListener/"]
COPY ["FurAniJoGa.Infrastructure/FurAniJoGa.Infrastructure.csproj", "FurAniJoGa.Infrastructure/"]
COPY ["FurAniJoGa.Domain/FurAniJoGa.Domain.csproj", "FurAniJoGa.Domain/"]
RUN dotnet restore "FurAniJoGa.MessagesListener/FurAniJoGa.MessagesListener.csproj"
COPY . .

WORKDIR "/src/FurAniJoGa.MessagesListener"
RUN dotnet build "FurAniJoGa.MessagesListener.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FurAniJoGa.MessagesListener.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FurAniJoGa.MessagesListener.dll"]
