FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["castledice-matchmaker/castledice-matchmaker.csproj", "castledice-matchmaker/"]
RUN dotnet restore "castledice-matchmaker/castledice-matchmaker.csproj"
COPY . .
WORKDIR "/src/castledice-matchmaker"
RUN dotnet build "castledice-matchmaker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "castledice-matchmaker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "castledice-matchmaker.dll"]
