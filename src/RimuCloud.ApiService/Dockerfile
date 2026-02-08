
# ===========================
#   STAGE 1: Build
# ===========================
FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
WORKDIR /src

# Copy all solution and file props
COPY --link ./src .
COPY ./Directory.Packages.props .

# Restore and publish
# RUN dotnet restore ./RimuCloud.ApiService/RimuCloud.ApiService.csproj
RUN --mount=type=cache,target=/root/.nuget,id=nuget-cache-apiservice \
    --mount=type=cache,target=/src/RimuCloud.ApiService/bin \
    --mount=type=cache,target=/src/RimuCloud.ApiService/obj \
    dotnet publish ./RimuCloud.ApiService/RimuCloud.ApiService.csproj -c Release -o /app/publish


# ===============================
# 2. Runtime stage
# ===============================
FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS final
RUN apk add --no-cache tzdata
WORKDIR /app
COPY --link --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "RimuCloud.ApiService.dll"]