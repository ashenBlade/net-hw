FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GrpcChat.Chat.Web/GrpcChat.Chat.Web.csproj", "GrpcChat.Chat.Web/"]
RUN dotnet restore "GrpcChat.Chat.Web/GrpcChat.Chat.Web.csproj"
COPY . .
WORKDIR "/src/GrpcChat.Chat.Web"
RUN dotnet build "GrpcChat.Chat.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GrpcChat.Chat.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GrpcChat.Chat.Web.dll"]
