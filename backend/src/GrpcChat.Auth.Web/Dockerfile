FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GrpcChat.Database/GrpcChat.Database.csproj", "GrpcChat.Database/"]
COPY ["GrpcChat.Domain/GrpcChat.Domain.csproj", "GrpcChat.Domain/"]
COPY ["GrpcChat.TokenGenerator/GrpcChat.TokenGenerator.csproj", "GrpcChat.TokenGenerator/"]
COPY ["GrpcChat.TokenGenerator.Jwt/GrpcChat.TokenGenerator.Jwt.csproj", "GrpcChat.TokenGenerator.Jwt/"]
COPY ["GrpcChat.Auth.Web/GrpcChat.Auth.Web.csproj", "GrpcChat.Auth.Web/"]
COPY ["GrpcChat.Auth.Web/GrpcChat.Auth.Web.csproj", "GrpcChat.Auth.Web/"]
RUN dotnet restore "GrpcChat.Auth.Web/GrpcChat.Auth.Web.csproj"
COPY . .
WORKDIR "/src/GrpcChat.Auth.Web"
RUN dotnet build "GrpcChat.Auth.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GrpcChat.Auth.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GrpcChat.Auth.Web.dll"]
