#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["host/Syrna.PrivateMessaging.Web.Unified/Syrna.PrivateMessaging.Web.Unified.csproj", "host/Syrna.PrivateMessaging.Web.Unified/"]
COPY ["src/Syrna.PrivateMessaging.Web/Syrna.PrivateMessaging.Web.csproj", "src/Syrna.PrivateMessaging.Web/"]
COPY ["src/Syrna.PrivateMessaging.HttpApi/Syrna.PrivateMessaging.HttpApi.csproj", "src/Syrna.PrivateMessaging.HttpApi/"]
COPY ["src/Syrna.PrivateMessaging.Application.Contracts/Syrna.PrivateMessaging.Application.Contracts.csproj", "src/Syrna.PrivateMessaging.Application.Contracts/"]
COPY ["src/Syrna.PrivateMessaging.Domain.Shared/Syrna.PrivateMessaging.Domain.Shared.csproj", "src/Syrna.PrivateMessaging.Domain.Shared/"]
COPY ["src/Syrna.PrivateMessaging.Application/Syrna.PrivateMessaging.Application.csproj", "src/Syrna.PrivateMessaging.Application/"]
COPY ["src/Syrna.PrivateMessaging.Domain/Syrna.PrivateMessaging.Domain.csproj", "src/Syrna.PrivateMessaging.Domain/"]
COPY ["src/Syrna.PrivateMessaging.EntityFrameworkCore/Syrna.PrivateMessaging.EntityFrameworkCore.csproj", "src/Syrna.PrivateMessaging.EntityFrameworkCore/"]
COPY ["host/Syrna.PrivateMessaging.Host.Shared/Syrna.PrivateMessaging.Host.Shared.csproj", "host/Syrna.PrivateMessaging.Host.Shared/"]
COPY Directory.Build.props .
RUN dotnet restore "host/Syrna.PrivateMessaging.Web.Unified/Syrna.PrivateMessaging.Web.Unified.csproj"
COPY . .
WORKDIR "/src/host/Syrna.PrivateMessaging.Web.Unified"
RUN dotnet build "Syrna.PrivateMessaging.Web.Unified.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Syrna.PrivateMessaging.Web.Unified.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Syrna.PrivateMessaging.Web.Unified.dll"]