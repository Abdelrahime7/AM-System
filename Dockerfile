# ============================================
# STAGE 1: BUILD
# ============================================
# Use specific version for reproducibility
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Set working directory
WORKDIR /src

# Copy solution and project files for better layer caching
COPY AMSystem.sln ./
COPY src/Application/Application.csproj ./src/Application/
COPY src/Domain/Domain.csproj ./src/Domain/
COPY src/Infrastructure/Infrastructure.csproj ./src/Infrastructure/
COPY src/WebAPI/WebAPI.csproj ./src/WebAPI/

# Restore packages in separate layer
RUN dotnet restore "src/WebAPI/WebAPI.csproj"

# Copy source code
COPY src/ ./src/

# Build and publish
WORKDIR /src/src/WebAPI
RUN dotnet build "WebAPI.csproj" -c Release -o /app/build 

# Publish the application (creates optimized runtime files)
RUN dotnet publish "WebAPI.csproj" -c Release -o /app/publish 

# ============================================
# STAGE 2: RUNTIME
# ============================================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

# Set working directory
WORKDIR /app


# Copy the published output from build stage
COPY --from=build /app/publish .

# Copy Docker-specific .env file
COPY .env.docker ./.env

# Expose port
EXPOSE 8080

# Set environment variable for ASP.NET Core
ENV ASPNETCORE_URLS=http://+:8080

# Use dotnet-counters for monitoring
ENTRYPOINT ["dotnet", "WebAPI.dll"]
