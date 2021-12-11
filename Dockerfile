FROM mcr.microsoft.com/dotnet/aspnet:6.0

ENV DOTNET_EnableDiagnostics=0

COPY bin/Debug/net6.0/publish/ App/
COPY Events.db App/
COPY Events.db-shm App/
COPY Events.db-wal App/

WORKDIR /App

ENV ASPNETCORE_URLS=http://*:5000

ENTRYPOINT ["dotnet", "Events.dll"]