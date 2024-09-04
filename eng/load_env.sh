source ./.env
export $(cat VERSION | xargs)
export ROOT_DIR=$(pwd)
export VERSION=$MAJOR.$MINOR.$PATCH
echo Current version: $VERSION
echo Root directory: $ROOT_DIR

