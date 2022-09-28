FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MessagesAPI/MessagesAPI.csproj", "MessagesAPI/"]
RUN dotnet restore "MessagesAPI/MessagesAPI.csproj"
COPY . .
WORKDIR "/src/MessagesAPI"
RUN dotnet build "MessagesAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MessagesAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MessagesAPI.dll"]
