# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["nuget.config", "."]
COPY ["src/BinanceSential.Auth.Web/BinanceSential.Auth.Web.csproj", "src/BinanceSential.Auth.Web/"]
COPY ["src/BinanceSential.Auth.Infrastructure/BinanceSential.Auth.Infrastructure.csproj", "src/BinanceSential.Auth.Infrastructure/"]
COPY ["src/BinanceSential.Auth.Core/BinanceSential.Auth.Core.csproj", "src/BinanceSential.Auth.Core/"]
COPY ["src/BinanceSential.Auth.UseCases/BinanceSential.Auth.UseCases.csproj", "src/BinanceSential.Auth.UseCases/"]
RUN dotnet restore "./src/BinanceSential.Auth.Web/BinanceSential.Auth.Web.csproj"
COPY . .
WORKDIR "/src/src/BinanceSential.Auth.Web"
RUN dotnet build "./BinanceSential.Auth.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BinanceSential.Auth.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Production
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BinanceSential.Auth.Web.dll"]