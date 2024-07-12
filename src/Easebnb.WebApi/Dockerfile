#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Easebnb.WebApi/Easebnb.WebApi.csproj", "Easebnb.WebApi/"]
COPY ["Easebnb.Application/Easebnb.Application.csproj", "Easebnb.Application/"]
COPY ["Easebnb.Domain/Easebnb.Domain.csproj", "Easebnb.Domain/"]
COPY ["Easebnb.Shared/Easebnb.Shared.csproj", "Easebnb.Shared/"]
COPY ["Easebnb.Infrastructure/Easebnb.Infrastructure.csproj", "Easebnb.Infrastructure/"]
RUN dotnet restore "Easebnb.WebApi/Easebnb.WebApi.csproj"
COPY . .
WORKDIR "/src/Easebnb.WebApi"
RUN dotnet build "./Easebnb.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Easebnb.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Easebnb.WebApi.dll"]