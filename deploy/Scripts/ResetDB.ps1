dotnet ef database update 0 --project "..\..\src\DblDip.Api\DblDip.Api.csproj"
dotnet ef migrations remove --project "..\..\src\DblDip.Api\DblDip.Api.csproj"
dotnet ef migrations add InitialCreate --project "..\..\src\DblDip.Api\DblDip.Api.csproj"
dotnet run ci --project "..\..\src\DblDip.Api\DblDip.Api.csproj"
