dotnet restore

dotnet build TauCode.Algorithms.Graphs.sln -c Debug
dotnet build TauCode.Algorithms.Graphs.sln -c Release

dotnet test TauCode.Algorithms.Graphs.sln -c Debug
dotnet test TauCode.Algorithms.Graphs.sln -c Release

nuget pack nuget\TauCode.Algorithms.Graphs.nuspec