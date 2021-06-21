#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["PortalMed.csproj", "."]
RUN dotnet restore "./PortalMed.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "PortalMed.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PortalMed.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PortalMed.dll"]