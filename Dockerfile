#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .

RUN dotnet restore "GymPlanner.WebApp/Project/GymPlanner.WebUI.csproj"

RUN dotnet tool install --global dotnet-ef --version 7.0.17
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet ef migrations add InitialCreate --project GymPlanner.WebApp/Infrastructure/GymPlanner.Infrastructure.csproj

RUN dotnet ef database update --project GymPlanner.WebApp/Infrastructure/GymPlanner.Infrastructure.csproj

WORKDIR "/src/GymPlanner.WebApp/Project"
RUN dotnet build "GymPlanner.WebUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GymPlanner.WebUI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "GymPlanner.WebUI.dll"]