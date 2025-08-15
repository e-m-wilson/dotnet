# --- Stage 1: Build and Publish ---

# We need to get our actual .NET SDK installed in this container
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# copy csproj file and restore dependencies
# COPY (File we need) (Target location in the container's file system)
COPY test-api.csproj ./ 
RUN dotnet restore

# copy everything else (our code, etc) and then build/publish
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# --- Stage 2: building a runtime image ---
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime 
WORKDIR /app

# copy published output 
COPY --from=build /app/publish ./

# Set ASPNETCORE_URLS for container 
ENV ASPNETCORE_URLS=http://+:80

# Expose a container port
EXPOSE 80

# entrypoint
ENTRYPOINT ["dotnet", "test-api.dll"]
