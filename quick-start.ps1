# quick-start.ps1 - One-command setup for Voucher Management System

Write-Host "Voucher Management System - Quick Start" -ForegroundColor Cyan
Write-Host "=========================================" -ForegroundColor Cyan
Write-Host ""

# Check if Docker is running
Write-Host "Checking Docker..." -ForegroundColor Yellow
try {
    docker --version | Out-Null
    Write-Host "Docker is available" -ForegroundColor Green
}
catch {
    Write-Host "Docker is not installed or not running" -ForegroundColor Red
    Write-Host "Please install Docker Desktop and try again" -ForegroundColor Red
    exit 1
}

# Check if .NET is installed
Write-Host "Checking .NET..." -ForegroundColor Yellow
try {
    dotnet --version | Out-Null
    Write-Host ".NET is available" -ForegroundColor Green
}
catch {
    Write-Host ".NET 8 SDK is not installed" -ForegroundColor Red
    Write-Host "Please install .NET 8 SDK and try again" -ForegroundColor Red
    exit 1
}

Write-Host ""
Write-Host "Starting services..." -ForegroundColor Cyan

# Start MongoDB
Write-Host "Starting MongoDB..." -ForegroundColor Yellow
docker-compose up mongodb -d
if ($LASTEXITCODE -ne 0) {
    Write-Host "Failed to start MongoDB" -ForegroundColor Red
    exit 1
}
Write-Host "MongoDB started successfully" -ForegroundColor Green

# Wait for MongoDB to be ready
Write-Host "Waiting for MongoDB to be ready..." -ForegroundColor Yellow
do {
    try {
        $result = docker exec voucher-mongodb mongosh --eval "db.runCommand('ping').ok" 2>$null
        if ($result -match "1") {
            break
        }
    }
    catch {
        # MongoDB not ready yet
    }
    Write-Host "   MongoDB is not ready yet, waiting..." -ForegroundColor Gray
    Start-Sleep 2
} while ($true)

Write-Host "MongoDB is ready!" -ForegroundColor Green

# Setup sample data  
Write-Host "Setting up sample data..." -ForegroundColor Yellow

Get-Content init-data.js | docker exec -i voucher-mongodb mongosh --username admin --password password123 --authenticationDatabase admin
if ($LASTEXITCODE -ne 0) {
    Write-Host "Failed to setup sample data" -ForegroundColor Red
    exit 1
}
Write-Host "Sample data setup completed" -ForegroundColor Green

# Build the application
Write-Host "Building application..." -ForegroundColor Yellow
dotnet build
if ($LASTEXITCODE -ne 0) {
    Write-Host "Build failed" -ForegroundColor Red
    exit 1
}
Write-Host "Application built successfully" -ForegroundColor Green

# Start the API
Write-Host "Starting API..." -ForegroundColor Yellow
Write-Host "   API will be available at: http://localhost:5158" -ForegroundColor Cyan
Write-Host "   Swagger UI: http://localhost:5158/swagger" -ForegroundColor Cyan
Write-Host ""
Write-Host "Press Ctrl+C to stop the API" -ForegroundColor Yellow
Write-Host ""

# Start API in foreground
dotnet run --project VoucherManagementSystem.API
