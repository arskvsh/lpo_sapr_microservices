#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["S2S_LMS/S2S_LMS.csproj", "S2S_LMS/"]
RUN dotnet restore "S2S_LMS/S2S_LMS.csproj"
COPY . .
WORKDIR "/src/S2S_LMS"
RUN dotnet build "S2S_LMS.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "S2S_LMS.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "S2S_LMS.dll"]