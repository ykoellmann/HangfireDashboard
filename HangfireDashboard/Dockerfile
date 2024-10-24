FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HangfireDashboard/HangfireDashboard.csproj", "HangfireDashboard/"]
RUN dotnet restore "HangfireDashboard/HangfireDashboard.csproj"
COPY . .
WORKDIR "/src/HangfireDashboard"
RUN dotnet build "HangfireDashboard.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HangfireDashboard.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HangfireDashboard.dll"]

ENV ConnectionStrings_DbConnection="User Id=postgres;Password=postgres;Server=192.168.178.41;Port=5432;Database=postgres;Integrated Security=true;Pooling=true;"