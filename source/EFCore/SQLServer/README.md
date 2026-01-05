# SQL Server Test




## References
- [Getting Started with Entity Framework Core [1 of 5]](https://learn.microsoft.com/en-us/shows/entity-framework-core-101/getting-started-with-entity-framework-core)
- [Entity Framework Core 시작(1/5)](https://kaki104.tistory.com/678)
- [Entity Framework Core를 사용하여 관계형 데이터 유지 및 검색](https://learn.microsoft.com/ko-kr/training/modules/persist-data-ef-core/?WT.mc_id=DT-MVP-5000651)




## Step
### Visual Studio Installer
- 추가 설치
- Data Storage and Processing workload
    - 데이터 스토리지 및 처리
- ASP.NET and web development workload
    - ASP.NET 및 웹 개발


### NuGet package 설치
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.Tools


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


