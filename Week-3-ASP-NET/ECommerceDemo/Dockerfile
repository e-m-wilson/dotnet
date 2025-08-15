# Stage 1 - Build and restore all project, via the .sln file
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build 
WORKDIR /src

# 1. COPY the .sln file
COPY ECommerceDemo.sln ./

#2. Copy each project file
COPY ECommerceDemo.API/ECommerceDemo.API.csproj ECommerceDemo.API/
COPY ECommerceDemo.Data/ECommerceDemo.Data.csproj ECommerceDemo.Data/
COPY ECommerceDemo.Models/ECommerceDemo.Models.csproj ECommerceDemo.Models/
COPY ECommerceDemo.Services/ECommerceDemo.Services.csproj ECommerceDemo.Services/
COPY ECommerceDemo.Tests/ECommerceDemo.Tests.csproj ECommerceDemo.Tests/

# 3. We restore packages for the entire solution
RUN dotnet restore

# 4. Copy the rest of the source dode
COPY . . 

# Publish ONLY the API project
RUN dotnet publish ECommerceDemo.API/ECommerceDemo.API.csproj -c Release -o /app/publish

# Stage 2: Runtime only to cut down on image/container size
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime 
WORKDIR /app

# Bring in the published API
COPY --from=build /app/publish ./

# Expose/configure ports
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

ENTRYPOINT [ "dotnet", "ECommerceDemo.API.dll" ]

