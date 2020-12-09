dotnet ef database update 0 --project "..\src\ShootQ.Api\ShootQ.Api.csproj"
dotnet ef migrations remove --project "..\src\ShootQ.Api\ShootQ.Api.csproj"
dotnet ef migrations add InitialCreate --project "..\src\ShootQ.Api\ShootQ.Api.csproj"
dotnet run ci --project "..\src\ShootQ.Api\ShootQ.Api.csproj"
