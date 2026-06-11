source eng/load_env.sh
pushd src/CC.CSX.Css
dotnet pack -c Release -o $ROOT_DIR/dist/packages /p:PackageVersion=$VERSION
popd
nuget add dist/packages/CC.CSX.Css.$VERSION.nupkg -source $NUGET_SOURCE
