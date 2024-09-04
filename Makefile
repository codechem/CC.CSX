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
	bash ./eng/package-core.sh

package.web:
	bash ./eng/package-web.sh

package.htmx: 
	bash ./eng/package-htmx.sh

publish.all: version.bump package.all
	bash ./eng/publish-all.sh
	git add .
	git commit -m "Publish Version ${VERSION}"

version.bump:
	./eng/version-bump.sh

