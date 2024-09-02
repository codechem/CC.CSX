build: 
	dotnet build

run.simple:
	dotnet run --project ./samples/Simple/Simple.csproj | bat -l html -p

run.htmx:
	dotnet run --project ./samples/HtmxSample/HtmxSample.csproj

run.web:
	dotnet run --project ./samples/Web/Web.csproj

watch.simple:
	dotnet watch --project ./samples/Simple/Simple.csproj | bat -l html -p

watch.htmx:
	dotnet watch --project ./samples/HtmxSample/HtmxSample.csproj

watch.web:
	dotnet watch --project ./samples/Web/Web.csproj
test:
	dotnet test

package.all: package.core package.web package.htmx
	echo "Package all done"


package.core:
	./eng/package-core.sh

package.web:
	./eng/package-web.sh

packag.htmx:
	./eng/package-htmx.sh

publish.all: version.bump package.all publish.core publish.web publish.htmx
	echo "Publish all done"

publish.core:
	source eng/load_env.sh && dotnet nuget push dist/packages/CC.CSX.$VERSION.nupkg -k $NUGET_API_KEY -s $NUGET_PUBLISH_SOURCE

publish.web:
	source eng/load_env.sh && dotnet nuget push dist/packages/CC.CSX.Web.$VERSION.nupkg -k $NUGET_API_KEY -s $NUGET_PUBLISH_SOURCE

publish.htmx:
	source eng/load_env.sh && dotnet nuget push dist/packages/CC.CSX.Htmx.$VERSION.nupkg -k $NUGET_API_KEY -s $NUGET_PUBLISH_SOURCE

version.bump:
	./eng/version-bump.sh

