# Entity Framework Core First

Install-Package Microsoft.EntityFrameworkCore.Sqlite
Install-Package Microsoft.EntityFrameworkCore.Tools

Add-Migration InitialCreate
Update-Database

C:/Users/leeyeonjun/AppData/Local/blogging.db

Scaffold-DbContext "Data Source=C:/Users/leeyeonjun/AppData/Local/blogging.db;" Microsoft.EntityFrameworkCore.Sqlite -OutputDir Models

