source eng/load_env.sh
dotnet nuget push dist/packages/CC.CSX.$VERSION.nupkg -k $NUGET_API_KEY -s $NUGET_PUBLISH_SOURCE
dotnet nuget push dist/packages/CC.CSX.Web.$VERSION.nupkg -k $NUGET_API_KEY -s $NUGET_PUBLISH_SOURCE
dotnet nuget push dist/packages/CC.CSX.Htmx.$VERSION.nupkg -k $NUGET_API_KEY -s $NUGET_PUBLISH_SOURCE
