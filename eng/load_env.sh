export NUGET_SOURCE="https://api.nuget.org/v3/index.json"
export $(cat VERSION | xargs)
export ROOT_DIR=$(pwd)
export VERSION=$MAJOR.$MINOR.$PATCH
echo Current version: $VERSION
echo Root directory: $ROOT_DIR

