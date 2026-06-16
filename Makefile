build: 
	dotnet build

run.simple:
	dotnet run --project ./samples/Simple/Simple.csproj | bat -l html -p

run.htmx:
	dotnet run --project ./samples/HtmxSample/HtmxSample.csproj

run.web:
	dotnet run --project ./samples/Web/Web.csproj

run.browser:
	dotnet run --project ./samples/BrowserSample/BrowserSample.csproj

watch.simple:
	dotnet watch --project ./samples/Simple/Simple.csproj | bat -l html -p

watch.htmx:
	dotnet watch --project ./samples/HtmxSample/HtmxSample.csproj

watch.web:
	dotnet watch --project ./samples/Web/Web.csproj

test:
	dotnet coverage collect "dotnet test --logger:\"junit;LogFilePath=./results/{assembly}-test-result.xml;MethodFormat=Class;FailureBodyFormat=Verbose\"" 

package.all: package.core package.web package.htmx package.css package.tailwind package.browser
	echo "Package all done"

package.core:
	bash ./eng/package-core.sh

package.web:
	bash ./eng/package-web.sh

package.htmx:
	bash ./eng/package-htmx.sh

package.css:
	bash ./eng/package-css.sh

package.tailwind:
	bash ./eng/package-tailwind.sh

package.browser:
	bash ./eng/package-browser.sh

publish.all: version.bump package.all
	bash ./eng/publish-all.sh
	git add .
	git commit -m "Publish Version ${VERSION}"

version.bump:
	./eng/version-bump.sh

