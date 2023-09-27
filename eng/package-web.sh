source eng/load_env.sh
pushd src/CC.CSX.Web
dotnet pack -c Release -o $ROOT_DIR/dist/packages /p:PackageVersion=$VERSION
popd
nuget add dist/packages/CC.CSX.Web.$VERSION.nupkg -source $NUGET_SOURCE
