#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TaxCalculations.API/TaxCalculations.API.csproj", "TaxCalculations.API/"]
COPY ["TaxCalculations.Application/TaxCalculations.Application.csproj", "TaxCalculations.Application/"]
COPY ["TaxCalculations.Domain/TaxCalculations.Domain.csproj", "TaxCalculations.Domain/"]
COPY ["TaxCalculations.Identity/TaxCalculations.Identity.csproj", "TaxCalculations.Identity/"]
COPY ["TaxCalculations.Persistence/TaxCalculations.Persistence.csproj", "TaxCalculations.Persistence/"]
RUN dotnet restore "TaxCalculations.API/TaxCalculations.API.csproj"
COPY . .
WORKDIR "/src/TaxCalculations.API"
RUN dotnet build "TaxCalculations.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaxCalculations.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaxCalculations.API.dll"]