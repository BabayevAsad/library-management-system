﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081     

RUN addgroup --system appgroup && adduser --system --ingroup appgroup appuser
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Api/Api.csproj", "Api/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]

RUN dotnet restore "Api/Api.csproj"

COPY . .

WORKDIR "/src/Api"
RUN dotnet build "Api.csproj" -c $BUILD_CONFIGURATION -o /app/build --no-restore

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "Api.dll"]
