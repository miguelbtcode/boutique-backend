FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["boutique.WebApi/boutique.WebApi.csproj", "boutique.WebApi/"]
RUN dotnet restore "boutique.WebApi/boutique.WebApi.csproj"
COPY . .
WORKDIR "/src/boutique.WebApi"
RUN dotnet build "boutique.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "boutique.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "boutique.WebApi.dll"]
