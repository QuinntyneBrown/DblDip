dotnet ef database update 0 --context DblDipDbContext --project "..\..\src\DblDip.Api\DblDip.Api.csproj"
dotnet ef migrations remove --context DblDipDbContext --project "..\..\src\DblDip.Api\DblDip.Api.csproj"
dotnet ef migrations add InitialCreate --context DblDipDbContext --project "..\..\src\DblDip.Api\DblDip.Api.csproj"

dotnet ef database update 0 --context EventStore --project "..\..\src\DblDip.Api\DblDip.Api.csproj"
dotnet ef migrations remove --context EventStore --project "..\..\src\DblDip.Api\DblDip.Api.csproj"
dotnet ef migrations add InitialCreate --context EventStore --project "..\..\src\DblDip.Api\DblDip.Api.csproj"

dotnet run ci --project "..\..\src\DblDip.Api\DblDip.Api.csproj"
