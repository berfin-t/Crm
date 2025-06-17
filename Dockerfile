FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

RUN apt-get update && apt-get install -y curl && \
    curl -fsSL https://deb.nodesource.com/setup_22.x | bash - && \
    apt-get install -y nodejs && \
    npm install -g npm

COPY . .

RUN dotnet tool install -g Volo.Abp.Cli --version 9.0.*
ENV PATH="$PATH:/root/.dotnet/tools"

RUN abp install-libs

RUN dotnet restore "src/Crm.Blazor/Crm.Blazor.csproj"
RUN dotnet publish "src/Crm.Blazor/Crm.Blazor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Crm.Blazor.dll"]
