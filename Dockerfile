FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 7070
EXPOSE 7071

# Force the app to listen on port 7070
ENV ASPNETCORE_URLS=http://+:7070

# 2. Build Image (Compile)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["eCommerce.ProductService.API/eCommerce.ProductService.API.csproj", "eCommerce.ProductService.API/"]
COPY ["eCommerce.ProductService.BLL/eCommerce.ProductService.BLL.csproj", "eCommerce.ProductService.BLL/"]
COPY ["eCommerce.ProductService.DAL/eCommerce.ProductService.DAL.csproj", "eCommerce.ProductService.DAL/"]

RUN dotnet restore "eCommerce.ProductService.API/eCommerce.ProductService.API.csproj"

COPY . .

# Build the API
WORKDIR "/src/eCommerce.ProductService.API"
RUN dotnet build "eCommerce.ProductService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the API
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "eCommerce.ProductService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# 3. Final Image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eCommerce.ProductService.API.dll"]