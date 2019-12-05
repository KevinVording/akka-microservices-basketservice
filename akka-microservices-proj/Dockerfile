FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["akka-microservices-proj/akka-microservices-proj.csproj", "akka-microservices-proj/"]
RUN dotnet restore "akka-microservices-proj/akka-microservices-proj.csproj"
COPY . .
WORKDIR "/src/akka-microservices-proj"
RUN dotnet build "akka-microservices-proj.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "akka-microservices-proj.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "akka-microservices-proj.dll"]