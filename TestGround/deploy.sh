git pull be main

build=$(dotnet build)

if [[ $? == 0 ]]; 
then
  publish=$(dotnet publish -c Release -o release)
  echo "Service released"
  elif [[ $? != 0 ]];
  then
    exit 1
fi 

if  [[ -d "release" ]];
then
  cd release  && ASPNETCORE_ENVIRONMENT=Development dotnet TestGround.dll
  else
    exit 1
fi 

  