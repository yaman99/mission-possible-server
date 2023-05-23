#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Auth/MissionPossible.API/MissionPossible.API.csproj", "Services/Auth/MissionPossible.API/"]
COPY ["Services/Auth/MissionPossible.Infrastructure/MissionPossible.Infrastructure.csproj", "Services/Auth/MissionPossible.Infrastructure/"]
COPY ["Services/Auth/MissionPossible.Shared/MissionPossible.Shared.csproj", "Services/Auth/MissionPossible.Shared/"]
COPY ["Services/Auth/MissionPossible.Application/MissionPossible.Application.csproj", "Services/Auth/MissionPossible.Application/"]
COPY ["Services/Auth/MissionPossible.Domain/MissionPossible.Domain.csproj", "Services/Auth/MissionPossible.Domain/"]
COPY ["BuildingBlocks/EmailSender.Service/EmailSender.Service.csproj", "BuildingBlocks/EmailSender.Service/"]
COPY ["BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "BuildingBlocks/EventBus.Messages/"]
RUN dotnet restore "Services/Auth/MissionPossible.API/MissionPossible.API.csproj"
COPY . .
WORKDIR "/src/Services/Auth/MissionPossible.API"
RUN dotnet build "MissionPossible.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MissionPossible.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MissionPossible.API.dll"]