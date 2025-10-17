FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["BE_project/BE_project.csproj", "BE_project/"]
RUN dotnet restore "BE_project/BE_project.csproj"

COPY . .
WORKDIR "/src/BE_project"
RUN dotnet build "BE_project.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BE_project.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "BE_project.dll"]