FROM mcr.microsoft.com/dotnet/sdk:8.0 AS s1
RUN dotnet tool install --global dotnet-ef


FROM s1 AS s2
RUN apt install openjdk-17-jdk -y

FROM s2 AS final
RUN dotnet tool install --global dotnet-coverage

ENTRYPOINT ["bash"]

