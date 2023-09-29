dotnet nuget push dist/packages/cc.csx.$VERSION.nupkg -k $NUGET_API_KEY -s $NUGET_PUBLISH_SOURCE
dotnet nuget push dist/packages/cc.csx.web.$VERSION.nupkg -k $NUGET_API_KEY -s $NUGET_PUBLISH_SOURCE
dotnet nuget push dist/packages/cc.csx.htmx.$VERSION.nupkg -k $NUGET_API_KEY -s $NUGET_PUBLISH_SOURCE
