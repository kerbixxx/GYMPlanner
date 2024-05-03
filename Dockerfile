#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["GymPlanner.WebApp/Project/GymPlanner.WebUI.csproj", "GymPlanner.WebApp/Project/"]
COPY ["GymPlanner.WebApp/Application/GymPlanner.Application.csproj", "GymPlanner.WebApp/Application/"]
COPY ["GymPlanner.WebApp/Domain/GymPlanner.Domain.csproj", "GymPlanner.WebApp/Domain/"]
COPY ["GymPlanner.WebApp/Infrastructure/GymPlanner.Infrastructure.csproj", "GymPlanner.WebApp/Infrastructure/"]
RUN dotnet restore "GymPlanner.WebApp/Project/GymPlanner.WebUI.csproj"
COPY . .
WORKDIR "/src/GymPlanner.WebApp/Project"
RUN dotnet build "GymPlanner.WebUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GymPlanner.WebUI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GymPlanner.WebUI.dll"]