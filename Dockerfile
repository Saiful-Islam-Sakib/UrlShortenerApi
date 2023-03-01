#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UrlShortener.API/UrlShortener.API.csproj", "UrlShortener.API/"]
COPY ["UrlShortener.Services/UrlShortener.Services.csproj", "UrlShortener.Services/"]
COPY ["UrlShortener.Repository/UrlShortener.Repository.csproj", "UrlShortener.Repository/"]
COPY ["UrlShortener.Shared/UrlShortener.Shared.csproj", "UrlShortener.Shared/"]
RUN dotnet restore "UrlShortener.API/UrlShortener.API.csproj"
COPY . .
WORKDIR "/src/UrlShortener.API"
RUN dotnet build "UrlShortener.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UrlShortener.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UrlShortener.API.dll"]