#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Courses/Courses.csproj", "."]
RUN dotnet restore "Courses.csproj"
COPY . .
WORKDIR "/src/Courses"
RUN dotnet build "Courses.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Courses.csproj" -c Release -o /app/publish

FROM base AS final
EXPOSE 5000
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Courses.dll"]