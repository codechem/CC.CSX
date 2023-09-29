source eng/load_env.sh
PATCH=$((PATCH + 1))
export VERSION=$MAJOR.$MINOR.$PATCH
echo "MAJOR=$MAJOR" > .env
echo "MINOR=$MINOR" >> .env
echo "PATCH=$PATCH" >> .env
echo "NUGET_SOURCE=$NUGET_SOURCE" >> .env
echo "Version bumped to $VERSION"
echo "NUGET_SOURCE=$NUGET_SOURCE"
