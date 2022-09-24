FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ForumAPI/ForumAPI.csproj", "ForumAPI/"]
RUN dotnet restore "ForumAPI/ForumAPI.csproj"
COPY . .
WORKDIR "/src/ForumAPI"
RUN dotnet build "ForumAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ForumAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ForumAPI.dll"]
