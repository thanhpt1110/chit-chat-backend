# See https://aka.ms/customizecontainer to learn how to customize your debug container 
# and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Stage 1: Base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Stage 2: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["ChitChat.WebAPI/ChitChat.WebAPI.csproj", "ChitChat.WebAPI/"]
COPY ["ChitChat.Infrastructure/ChitChat.Infrastructure.csproj", "ChitChat.Infrastructure/"]
COPY ["ChitChatApplication/ChitChat.Application.csproj", "ChitChatApplication/"]
COPY ["ChitChat.DataAccess/ChitChat.DataAccess.csproj", "ChitChat.DataAccess/"]
COPY ["ChitChat.Domain/ChitChat.Domain.csproj", "ChitChat.Domain/"]

RUN dotnet restore "ChitChat.WebAPI/ChitChat.WebAPI.csproj"

COPY . .
WORKDIR "/src/ChitChat.WebAPI"
RUN dotnet build "ChitChat.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Stage 3: Publish
FROM build AS publish
RUN dotnet publish "ChitChat.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Stage 4: Final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChitChat.WebAPI.dll"]
