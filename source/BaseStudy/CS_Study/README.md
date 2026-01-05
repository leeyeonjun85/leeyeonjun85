

- 주석 추가 : Ctrl+KC
- 주석 제거 : Ctrl+KU
- 솔루션 창 열기 : Ctrl+Alt+L
- Tool Window 전환 : Shift+Alt+F6
- 책갈피 : Ctrl+KK
- 변수이름변경 : Ctrl+RR


///////////////////////////////////////////////////////////////////////////////////////////////////////////


## Reference
- [Visual Studio 자습서 C#](https://learn.microsoft.com/ko-kr/visualstudio/get-started/csharp/?view=vs-2022)


///////////////////////////////////////////////////////////////////////////////////////////////////////////


## Calcuator
### 자습서: Visual Studio에서 간단한 C# 콘솔 앱 만들기(1/2부)
https://learn.microsoft.com/ko-kr/visualstudio/get-started/csharp/tutorial-console?view=vs-2022

### 자습서: C# 콘솔 앱 확장 및 Visual Studio에서 디버그(2/2부)
https://learn.microsoft.com/ko-kr/visualstudio/get-started/csharp/tutorial-console-part-2?view=vs-2022


///////////////////////////////////////////////////////////////////////////////////////////////////////////


## CS_Study

### Hello World - C# 소개 대화형 C# 자습서
https://learn.microsoft.com/ko-kr/dotnet/csharp/tour-of-csharp/tutorials/hello-world

### C#에서 정수 및 부동 소수점 수 조작
https://learn.microsoft.com/ko-kr/dotnet/csharp/tour-of-csharp/tutorials/numbers-in-csharp

### 분기 및 루프 문이 포함된 조건부 논리 알아보기
https://learn.microsoft.com/ko-kr/dotnet/csharp/tour-of-csharp/tutorials/branches-and-loops

### 일반 목록 형식을 사용하여 데이터 컬렉션을 관리하는 방법 알아보기
https://learn.microsoft.com/ko-kr/dotnet/csharp/tour-of-csharp/tutorials/list-collection


///////////////////////////////////////////////////////////////////////////////////////////////////////////


## EFCore_MVC
### C# Entity Framework6(EF6) 강의 7편 (Repository Pattern, DI)
https://www.youtube.com/watch?v=NM-bqkmrac4&list=PLlrfTSXS0LLKYjSrTuGlvB4mPd3vv8t4f&index=10
https://github.com/KaburiCoder/EF6Basic


///////////////////////////////////////////////////////////////////////////////////////////////////////////


## EFCore_MySQL
### C# Entity Framework6(EF6) 강의 4편(CREATE) 데이터 삽입
https://www.youtube.com/watch?v=RMuPZ6omO2U&list=PLlrfTSXS0LLKYjSrTuGlvB4mPd3vv8t4f&index=6

Install-Package MySql.EntityFrameworkCore


////


## EFCore_SQLServer
- [Getting Started with Entity Framework Core [1 of 5]](https://learn.microsoft.com/en-us/shows/entity-framework-core-101/getting-started-with-entity-framework-core)
- [Entity Framework Core 시작(1/5)](https://kaki104.tistory.com/678)
- [Entity Framework Core를 사용하여 관계형 데이터 유지 및 검색](https://learn.microsoft.com/ko-kr/training/modules/persist-data-ef-core/?WT.mc_id=DT-MVP-5000651)

### Visual Studio Installer
- 추가 설치
- Data Storage and Processing workload
    - 데이터 스토리지 및 처리
- ASP.NET and web development workload
    - ASP.NET 및 웹 개발

### NuGet package 설치
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Design
Install-Package Microsoft.EntityFrameworkCore.Tools


### Models 폴더 추가
- Models 폴더에 Product, Customer, Order, ProductOrder 클래스 추가

    - Product.cs
```cs
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQLServer.Models
{
    internal class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}

```

    - Customer.cs
```cs
namespace SQLServer.Models
{
    public class Customer
    {
#nullable enable
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
#nullable disable
        public ICollection<Order> Orders { get; set; }
    }
}
```

    - Order.cs
```cs
namespace SQLServer.Models
{
    internal class Order
    {
        public int Id { get; set; }
        public DateTime OrderPlaced { get; set; }
        public DateTime? OrderFulfilled { get; set; }
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
        public ICollection<ProductOrder> ProductOrders { get; set; }
    }
}
```

    - ProductOrder.cs
```cs
namespace SQLServer.Models
{
    internal class ProductOrder
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
```


### Data 폴더 추가
- ContosoPetsContext.cs
```cs
using Microsoft.EntityFrameworkCore;
using SQLServer.Models;

namespace SQLServer.Data
{
    public class ContosoPetsContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //셈플이기 때문에 여기에 connection string을 입력
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TestDB;Integrated Security=true");
        }
    }
}

```


### Package Manager Console
```bash
# 모델 생성
Add-Migration InitialCreate

# 마이그레이션 실행
Update-Database
```
























## Names
### 자습서: .NET을 사용하여 새 WPF 앱 만들기
https://learn.microsoft.com/ko-kr/dotnet/desktop/wpf/get-started/create-app-visual-studio?view=netdesktop-7.0




의존성 주입
https://learn.microsoft.com/ko-kr/dotnet/core/extensions/dependency-injection-guidelines
Install-Package Microsoft.Extensions.Hosting
Install-Package Microsoft.EntityFrameworkCore.Sqlite
Install-Package Oracle.EntityFrameworkCore


- [Visual Studio 자습서 C#](https://learn.microsoft.com/ko-kr/visualstudio/get-started/csharp/?view=vs-2022)
- [자습서: Visual Studio를 사용하여 .NET 콘솔 애플리케이션 만들기](https://learn.microsoft.com/ko-kr/dotnet/core/tutorials/with-visual-studio?pivots=dotnet-7-0)

