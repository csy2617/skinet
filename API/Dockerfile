# Stage 1: Use the base ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

# Stage 2: Copy the prebuilt .NET Core and Angular code
# Copy the .NET Core app from the 'publish' folder
COPY ./publish /app

# Copy the Angular frontend from the 'wwwroot' folder
COPY ./wwwroot /app/wwwroot

# ENTRYPOINT to run the .NET Core application
ENTRYPOINT ["dotnet", "API.dll"]
