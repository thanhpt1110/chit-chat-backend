# Stage 1: Base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Stage 2: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Cập nhật đường dẫn với `src`
COPY src/**/*.csproj ./
RUN dotnet restore "ChitChat.WebAPI/ChitChat.WebAPI.csproj"

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

# Wait for MySQL and then start the app
ENTRYPOINT ["sh", "-c", "until nc -z -v -w30 mysql 3306; do echo 'Waiting for MySQL...'; sleep 1; done && dotnet ChitChat.WebAPI.dll"]
