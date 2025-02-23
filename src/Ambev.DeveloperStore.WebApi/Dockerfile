#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Ambev.DeveloperStore.WebApi/Ambev.DeveloperStore.WebApi.csproj", "src/Ambev.DeveloperStore.WebApi/"]
COPY ["src/Ambev.DeveloperStore.IoC/Ambev.DeveloperStore.IoC.csproj", "src/Ambev.DeveloperStore.IoC/"]
COPY ["src/Ambev.DeveloperStore.Domain/Ambev.DeveloperStore.Domain.csproj", "src/Ambev.DeveloperStore.Domain/"]
COPY ["src/Ambev.DeveloperStore.Common/Ambev.DeveloperStore.Common.csproj", "src/Ambev.DeveloperStore.Common/"]
COPY ["src/Ambev.DeveloperStore.Application/Ambev.DeveloperStore.Application.csproj", "src/Ambev.DeveloperStore.Application/"]
COPY ["src/Ambev.DeveloperStore.ORM/Ambev.DeveloperStore.ORM.csproj", "src/Ambev.DeveloperStore.ORM/"]
RUN dotnet restore "./src/Ambev.DeveloperStore.WebApi/Ambev.DeveloperStore.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Ambev.DeveloperStore.WebApi"
RUN dotnet build "./Ambev.DeveloperStore.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Ambev.DeveloperStore.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ambev.DeveloperStore.WebApi.dll"]