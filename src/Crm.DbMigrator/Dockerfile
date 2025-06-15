FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .

RUN dotnet restore "src/Crm.DbMigrator/Crm.DbMigrator.csproj"
RUN dotnet publish "src/Crm.DbMigrator/Crm.DbMigrator.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Crm.DbMigrator.dll"]