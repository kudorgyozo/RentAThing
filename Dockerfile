# Stage 1: Build the Angular application
FROM node:lts AS frontend-build

# Set the working directory
WORKDIR /app

# Copy package files and install dependencies
COPY RentAThing.Client/package.json ./
RUN npm install -g pnpm
RUN pnpm install

# Copy the rest of the application source
COPY RentAThing.Client/ .

# Build the Angular app
RUN pnpm run build


# Stage 2: Build the .NET API
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS backend-build

# Set the working directory
WORKDIR /src

# Optimization? Copy csproj only first and restore
COPY RentAThing.Server/RentAThing.Server.csproj .
RUN dotnet restore

COPY RentAThing.Server/ .
# Copy the built Angular app to the wwwroot folder of the API
COPY --from=frontend-build /app/dist/ ./wwwroot

#RUN dotnet build -c Release -o /app/build
RUN dotnet publish -c Release -o /app/publish

# Stage 3: Run the .NET API
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

# Set the working directory
WORKDIR /app

# Copy the published output from the build stage
COPY --from=backend-build /app/publish .

# Expose port 80
EXPOSE 8080

# Run the .NET API
ENTRYPOINT ["dotnet", "RentAThing.Server.dll"]