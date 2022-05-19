#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DisasterTracker/DisasterTracker.csproj", "DisasterTracker/"]
COPY ["DisasterTracker.BL/DisasterTracker.BL.csproj", "DisasterTracker.BL/"]
COPY ["DisasterTracker.DataServices/DisasterTracker.DataServices.csproj", "DataServices/"]
COPY ["DisasterTracker.Data/DisasterTracker.Data.csproj", "DisasterTracker.Data/"]
COPY ["DisasterTracker.PdcApiModels/DisasterTracker.PdcApiModels.csproj", "DisasterTracker.PdcApiModels/"]
RUN dotnet restore "DisasterTracker/DisasterTracker.csproj"
COPY . .
WORKDIR "/src/DisasterTracker"
RUN dotnet build "DisasterTracker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DisasterTracker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ["dotnet", "DisasterTracker.dll"]