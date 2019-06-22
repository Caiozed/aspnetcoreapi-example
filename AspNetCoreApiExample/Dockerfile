FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["AspNetCoreApiExample/AspNetCoreApiExample.csproj", "AspNetCoreApiExample/"]
RUN dotnet restore "AspNetCoreApiExample/AspNetCoreApiExample.csproj"
COPY . .
WORKDIR "/src/AspNetCoreApiExample"
RUN dotnet build "AspNetCoreApiExample.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "AspNetCoreApiExample.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AspNetCoreApiExample.dll"]
