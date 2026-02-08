# ===========================
#   STAGE 1: Build
# ===========================
FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
WORKDIR /src

# Copy all solution and file props
COPY --link ./src .
COPY ./Directory.Packages.props .

# Restore and publish
# RUN dotnet restore ./RimuCloud.MigrationService/RimuCloud.MigrationService.csproj
RUN --mount=type=cache,target=/root/.nuget,id=nuget-cache-migration \
    --mount=type=cache,target=/src/RimuCloud.MigrationService/bin \
    --mount=type=cache,target=/src/RimuCloud.MigrationService/obj \
    dotnet publish ./RimuCloud.MigrationService/RimuCloud.MigrationService.csproj -c Release -o /app/publish

# ===========================
#   STAGE 2: Runtime
# ===========================
FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS final
RUN apk add --no-cache tzdata
WORKDIR /app
COPY --link --from=build /app/publish .
ENTRYPOINT ["dotnet", "RimuCloud.MigrationService.dll"]
