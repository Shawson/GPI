dotnet ef --startup-project GPI.Api/GPI.Api.csproj migrations add InitialModel -p GPI.Data/GPI.Data.csproj
dotnet ef --startup-project GPI.Api/GPI.Api.csproj database update