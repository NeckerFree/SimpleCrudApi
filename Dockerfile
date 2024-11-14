
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SimpleCrudApi/SimpleCrudApi.csproj", "SimpleCrudApi/"]
RUN dotnet restore "SimpleCrudApi/SimpleCrudApi.csproj"
COPY . .
WORKDIR "/src/SimpleCrudApi"
RUN dotnet build "SimpleCrudApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SimpleCrudApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "SimpleCrudApi.dll"]
