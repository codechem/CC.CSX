source eng/load_env.sh
pushd src/CC.CSX
dotnet pack -c Release -o $ROOT_DIR/dist/packages /p:PackageVersion=$VERSION
popd
nuget add dist/packages/CC.CSX.$VERSION.nupkg -source $NUGET_SOURCE



