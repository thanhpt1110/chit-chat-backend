# Stage 1: Base
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Stage 2: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files and restore dependencies
COPY ["src/ChitChat.WebAPI/ChitChat.WebAPI.csproj", "ChitChat.WebAPI/"]
COPY ["src/ChitChat.Infrastructure/ChitChat.Infrastructure.csproj", "ChitChat.Infrastructure/"]
COPY ["src/ChitChat.Application/ChitChat.Application.csproj", "ChitChat.Application/"]
COPY ["src/ChitChat.DataAccess/ChitChat.DataAccess.csproj", "ChitChat.DataAccess/"]
COPY ["src/ChitChat.Domain/ChitChat.Domain.csproj", "ChitChat.Domain/"]
RUN dotnet restore "ChitChat.WebAPI/ChitChat.WebAPI.csproj"

# Copy the remaining source code and build
COPY src/ . 
WORKDIR "/src/ChitChat.WebAPI"
RUN dotnet build "ChitChat.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Stage 3: Publish
FROM build AS publish
RUN dotnet publish "ChitChat.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Stage 4: Final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Copy SSL certificate if Production
ARG ENVIRONMENT=Development
COPY ["src/ChitChat.WebAPI/chitchat.pfx", "/app/chitchat.pfx"]
RUN if [ "$ENVIRONMENT" = "Production" ]; then cp /app/chitchat.pfx /app/chitchat.pfx; fi

ENTRYPOINT ["dotnet", "ChitChat.WebAPI.dll"]
