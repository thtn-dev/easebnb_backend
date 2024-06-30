# Migration DB


```
 dotnet ef migrations add InitialIdentity -c AppDbContext --project .\Easebnb.Infrastructure\Easebnb.Infrastructure.csproj --startup-project .\Easebnb.WebApi\Easebnb.WebApi.csproj -o Data/Migrations/App
```

```
dotnet ef database update -c AppDbContext --project .\Easebnb.Infrastructure\Easebnb.Infrastructure.csproj --startup-project .\Easebnb.WebApi\Easebnb.WebApi.csproj
```

```
dotnet ef migrations remove -c AppDbContext --project .\Easebnb.Infrastructure\Easebnb.Infrastructure.csproj --startup-project .\Easebnb.WebApi\Easebnb.WebApi.csproj
```