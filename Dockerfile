FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8081
ENV ASPNETCORE_URLS=http://+:8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src
COPY ["src/TMS.Notes.Core/TMS.Notes.Core.csproj", "TMS.Notes.Core/"]
COPY ["src/TMS.Notes.Service/TMS.Notes.Service.csproj", "TMS.Notes.Service/"]
COPY ["src/TMS.Notes.DataAccess/TMS.Notes.DataAccess.csproj", "TMS.Notes.DataAccess/"]
COPY ["src/TMS.Notes.UseCases/TMS.Notes.UseCases.csproj", "TMS.Notes.UseCases/"]

WORKDIR /src/TMS.Notes.Service
RUN dotnet restore "TMS.Notes.Service.csproj"

COPY . .
RUN dotnet build "TMS.Notes.Service.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TMS.Notes.Service.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TMS.Notes.Service.dll"]
