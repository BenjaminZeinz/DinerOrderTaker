dotnet build OrderTaker\OrderTaker.csproj
dotnet build Console\Console.csproj
dotnet build Tests\Tests.csproj
dotnet test Tests\Tests.csproj
dotnet run --project Console\