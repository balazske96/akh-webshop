FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["backend/AKHWebshop/AKHWebshop/AKHWebshop.csproj", "backend/AKHWebshop/AKHWebshop/"]
RUN dotnet restore "backend/AKHWebshop/AKHWebshop/AKHWebshop.csproj"
COPY . .
WORKDIR "/src/backend/AKHWebshop/AKHWebshop"
RUN dotnet build "AKHWebshop.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AKHWebshop.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AKHWebshop.dll"]
