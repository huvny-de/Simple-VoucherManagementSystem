# Use the official .NET 8 runtime as base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the official .NET 8 SDK for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files
COPY ["VoucherManagementSystem.API/VoucherManagementSystem.API.csproj", "VoucherManagementSystem.API/"]
COPY ["VoucherManagementSystem.Application/VoucherManagementSystem.Application.csproj", "VoucherManagementSystem.Application/"]
COPY ["VoucherManagementSystem.Domain/VoucherManagementSystem.Domain.csproj", "VoucherManagementSystem.Domain/"]
COPY ["VoucherManagementSystem.Infrastructure/VoucherManagementSystem.Infrastructure.csproj", "VoucherManagementSystem.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "VoucherManagementSystem.API/VoucherManagementSystem.API.csproj"

# Copy all source code
COPY . .

# Build the application
WORKDIR "/src/VoucherManagementSystem.API"
RUN dotnet build "VoucherManagementSystem.API.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "VoucherManagementSystem.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set environment variables
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:80

ENTRYPOINT ["dotnet", "VoucherManagementSystem.API.dll"]
