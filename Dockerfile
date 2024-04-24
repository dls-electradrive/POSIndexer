#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

#variables for db and messagequeue
ENV DB_CONNECTION_STRING "server=host.docker.internal;port=3307;uid=root;pwd=12345;database=carstorage"
ENV DB_USER "guest"
ENV DB_PASS "12345"

ENV MQ_HOSTNAME "host.docker.internal"
ENV MQ_USER "guest"
ENV MQ_PASS "guest"

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["POSIndexer.csproj", "."]
RUN dotnet restore "./././POSIndexer.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./POSIndexer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./POSIndexer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "POSIndexer.dll"]