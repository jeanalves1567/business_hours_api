FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app
COPY *.sln .
COPY src/BusinessHours.Api/*.csproj ./src/BusinessHours.Api/
COPY src/BusinessHours.CrossCutting/*.csproj ./src/BusinessHours.CrossCutting/
COPY src/BusinessHours.Data/*.csproj ./src/BusinessHours.Data/
COPY src/BusinessHours.Domain/*.csproj ./src/BusinessHours.Domain/
COPY src/BusinessHours.Service/*.csproj ./src/BusinessHours.Service/
RUN dotnet restore
COPY . ./
RUN dotnet build
RUN dotnet publish src/BusinessHours.Api -c Release -o publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/publish .
ENTRYPOINT ["dotnet", "BusinessHours.Api.dll"]
