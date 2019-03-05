FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["SkillTest.WorkingDays/SkillTest.WorkingDays.csproj", "SkillTest.WorkingDays/"]
RUN dotnet restore "SkillTest.WorkingDays/SkillTest.WorkingDays.csproj"
COPY . .
WORKDIR "/src/SkillTest.WorkingDays"
RUN dotnet build "SkillTest.WorkingDays.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SkillTest.WorkingDays.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SkillTest.WorkingDays.dll"]