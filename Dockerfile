FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .

WORKDIR /src/Library.API
RUN dotnet publish Library.API.csproj -c Release -o /app/publish

FROM build AS tests
WORKDIR /src/Library.Tests
RUN dotnet test

FROM build AS update-database
WORKDIR /src
RUN dotnet tool install --global dotnet-ef
RUN dotnet new tool-manifest --force
RUN dotnet tool install dotnet-ef
RUN dotnet tool restore
ENV PATH $PATH:/root/.dotnet/tools
RUN dotnet ef database update --project Library.Infrastructure --startup-project Library.API

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=update-database /src/Library.API/dbo.Library.db .
COPY --from=update-database /app/publish .
ENTRYPOINT ["dotnet", "Library.API.dll"]