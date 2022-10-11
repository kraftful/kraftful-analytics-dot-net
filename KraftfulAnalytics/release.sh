# Clean the release directory
rm -rf ./bin/Release
# Do the msbuild for all frameworks
dotnet msbuild /t:PublishAll /p:Configuration=Release
# Copy over the README
cp ../README.md ./bin/Release/netstandard2.0/publish
cp ../README.md ./bin/Release/netstandard2.1/publish
# Zip up the 2.0 files
pushd ./bin/Release/netstandard2.0/publish
zip ../../../../KraftfulAnalytics.NetStandard2.0-vX.X.X.zip -r ./*
popd
# Zip up the 2.1 files
pushd ./bin/Release/netstandard2.1/publish
zip ../../../../KraftfulAnalytics.NetStandard2.1-vX.X.X.zip -r ./*
popd