#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Services/ReportingDwh/ReportingDwhApi/ReportingDwhApi.csproj", "src/Services/ReportingDwh/ReportingDwhApi/"]
RUN dotnet restore "src/Services/ReportingDwh/ReportingDwhApi/ReportingDwhApi.csproj"
COPY . .
WORKDIR "/src/src/Services/ReportingDwh/ReportingDwhApi"
RUN dotnet build "ReportingDwhApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReportingDwhApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReportingDwhApi.dll"]