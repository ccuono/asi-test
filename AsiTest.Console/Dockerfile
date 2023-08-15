FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AsiTest.Business.Console/AsiTest.Business.Console.csproj", "AsiTest.Business.Console/"]
RUN dotnet restore "AsiTest.Business.Console/AsiTest.Business.Console.csproj"
COPY . .
WORKDIR "/src/AsiTest.Business.Console"
RUN dotnet build "AsiTest.Business.Console.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AsiTest.Business.Console.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AsiTest.Business.Console.dll"]
