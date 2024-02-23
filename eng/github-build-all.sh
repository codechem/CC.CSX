source eng/load_env.sh && \
dotnet build  src/CC.CSX/CC.CSX.csproj -c Release && \

dotnet add src/CC.CSX.Web/CC.CSX.Web.csproj package CC.CSX --version $VERSION --source $NUGET_SOURCE && \
dotnet build src/CC.CSX.Web/CC.CSX.Web.csproj -c Release && \

dotnet add src/CC.CSX.Htmx/CC.CSX.Htmx.csproj package CC.CSX --version $VERSION --source $NUGET_SOURCE && \
dotnet build src/CC.CSX.Htmx/CC.CSX.Htmx.csproj -c Release
