FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["eCommerce.ProductService.API/eCommerce.ProductService.API.csproj", "eCommerce.ProductService.API/"]
RUN dotnet restore "eCommerce.ProductService.API/eCommerce.ProductService.API.csproj"
COPY . .
WORKDIR "/src/eCommerce.ProductService.API"
RUN dotnet build "./eCommerce.ProductService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./eCommerce.ProductService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
ENV MYSQL_HOST=localhost
ENV MYSQL_PASSWORD=Legion13$%
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eCommerce.ProductService.API.dll"]
