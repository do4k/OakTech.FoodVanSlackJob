FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OakTech.FoodVanSlackJob/OakTech.FoodVanSlackJob.csproj", "OakTech.FoodVanSlackJob/"]
RUN dotnet restore "OakTech.FoodVanSlackJob/OakTech.FoodVanSlackJob.csproj"
COPY . .
WORKDIR "/src/OakTech.FoodVanSlackJob"
RUN dotnet build "OakTech.FoodVanSlackJob.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "OakTech.FoodVanSlackJob.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OakTech.FoodVanSlackJob.dll"]
