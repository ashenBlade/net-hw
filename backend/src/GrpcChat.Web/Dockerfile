FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GrpcChat.Web/GrpcChat.Web.csproj", "GrpcChat.Web/"]
RUN dotnet restore "GrpcChat.Web/GrpcChat.Web.csproj"
COPY . .
WORKDIR "/src/GrpcChat.Web"
RUN dotnet build "GrpcChat.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GrpcChat.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GrpcChat.Web.dll"]
