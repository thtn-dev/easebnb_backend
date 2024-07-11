#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Easebnb.WebApi/Easebnb.WebApi.csproj", "src/Easebnb.WebApi/"]
COPY ["src/Easebnb.Application/Easebnb.Application.csproj", "src/Easebnb.Application/"]
COPY ["src/Easebnb.Domain/Easebnb.Domain.csproj", "src/Easebnb.Domain/"]
COPY ["src/Easebnb.Shared/Easebnb.Shared.csproj", "src/Easebnb.Shared/"]
COPY ["src/Easebnb.Infrastructure/Easebnb.Infrastructure.csproj", "src/Easebnb.Infrastructure/"]
RUN dotnet restore "./src/Easebnb.WebApi/Easebnb.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Easebnb.WebApi"
RUN dotnet build "./Easebnb.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Easebnb.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Easebnb.WebApi.dll"]