FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["react.pizza.backend/react.pizza.backend.csproj", "react.pizza.backend/"]
RUN dotnet restore "react.pizza.backend/react.pizza.backend.csproj"
COPY . .
WORKDIR "/src/react.pizza.backend"
RUN dotnet build "react.pizza.backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "react.pizza.backend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "react.pizza.backend.dll"]
