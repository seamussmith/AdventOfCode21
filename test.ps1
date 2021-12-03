
$proj_name = Split-Path -Path $pwd -Leaf
$a = $args[0]
dotnet test --logger:"console;verbosity=detailed" ./$proj_name.Day$a.Test/
