# Install requirements

```
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design
```

# Creating database

```
dotnet ef migrations add InitialCreate
dotnet ef database update InitialCreate
```
