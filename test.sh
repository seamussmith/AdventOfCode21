
proj_name=${PWD##*/}
dotnet test --logger:"console;verbosity=detailed" ./$proj_name.Day$1.Test/
