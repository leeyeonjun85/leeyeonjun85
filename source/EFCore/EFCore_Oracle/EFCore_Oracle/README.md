### Oracle_EFCore

- Visual Studio 하단에 `패키지 관리자 콘솔`이 없는 경우
	- `도구 - NeGet 패키지 관리자 - 패키지 관리자 콘솔` 클릭

#### 필요한 NuGet 패키지
Microsoft.EntityFrameworkCore.Tools
Oracle.EntityFrameworkCore

Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package Oracle.EntityFrameworkCore

#### Model Migration
```PM
add-migration createTable
update-database
```

#### 오라클 DB에서 Models 가져오기
```PM
Scaffold-DbContext "User Id=testuser1;Password=0330;Data Source=localhost:1521/XEPDB1;" Oracle.EntityFrameworkCore -OutputDir Models
```

User Id=testuser1;Password=0330;Data Source=localhost:1521/XEPDB1;

#### References
- [Oracle Entity Framework (EF) Core 6 Demo](https://www.youtube.com/watch?v=iyHDODskDlk)


