# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy everything from the project folder
COPY . .

# Restore and publish
RUN dotnet restore "./PersonalGrowthTracker.Api.csproj"
RUN dotnet publish "./PersonalGrowthTracker.Api.csproj" -c Release -o /out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /out .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "PersonalGrowthTracker.Api.dll"]
