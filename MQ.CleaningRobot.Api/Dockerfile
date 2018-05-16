FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY MQ.CleaningRobot.Api/MQ.CleaningRobot.Api.csproj MQ.CleaningRobot.Api/
RUN dotnet restore MQ.CleaningRobot.Api/MQ.CleaningRobot.Api.csproj
COPY . .
WORKDIR /src/MQ.CleaningRobot.Api
RUN dotnet build MQ.CleaningRobot.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish MQ.CleaningRobot.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MQ.CleaningRobot.Api.dll"]
