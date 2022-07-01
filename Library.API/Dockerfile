FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Library.API/Library.API.csproj", "Library.API/"]
RUN dotnet restore "Library.API/Library.API.csproj"
COPY . .
WORKDIR "/src/Library.API"
RUN dotnet build "Library.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Library.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Library.API.dll"]
