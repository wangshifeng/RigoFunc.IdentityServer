#!/usr/bin/env bash

# ref http://andrewlock.net/adding-travis-ci-to-a-net-core-app/
#exit if any command fails
set -e

apt-get install libunwind8 -y

wget https://raw.githubusercontent.com/dotnet/cli/rel/1.0.0-preview1/scripts/obtain/dotnet-install.sh
chmod a+x dotnet-install.sh
./dotnet-install.sh --channel preview

artifactsFolder="./artifacts"

if [ -d $artifactsFolder ]; then  
  rm -R $artifactsFolder
fi

dotnet restore
 
#dotnet test ./src/Host -c Debug -f netcoreapp1.0
dotnet build ./src/Host -c Debug -f netcoreapp1.0

#dotnet test ./src/RigoFunc.IdentityServer -c Debug -f netcoreapp1.0
dotnet build ./src/RigoFunc.IdentityServer Host -c Debug -f netcoreapp1.0


revision=${TRAVIS_JOB_ID:=1}  
revision=$(printf "%04d" $revision) 

#dotnet pack ./src/RigoFunc.IdentityServer -c Debug -o ./artifacts --version-suffix=$revision  