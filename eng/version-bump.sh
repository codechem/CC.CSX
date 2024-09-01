source eng/load_env.sh
PATCH=$((PATCH + 1))
export VERSION=$MAJOR.$MINOR.$PATCH
echo "MAJOR=$MAJOR" > VERSION
echo "MINOR=$MINOR" >> VERSION
echo "PATCH=$PATCH" >> VERSION
echo "VERSION=$VERSION"
