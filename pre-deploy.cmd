dotnet restore

dotnet build --configuration Debug
dotnet build --configuration Release

dotnet test -c Debug .\test\TauCode.Algorithms.Graphs.Tests\TauCode.Algorithms.Graphs.Tests.csproj
dotnet test -c Release .\test\TauCode.Algorithms.Graphs.Tests\TauCode.Algorithms.Graphs.Tests.csproj

nuget pack nuget\TauCode.Algorithms.Graphs.nuspec