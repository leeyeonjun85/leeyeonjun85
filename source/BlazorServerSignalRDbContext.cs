using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorServerSignalR.Data
{
    public class BlazorServerSignalRDbContext : IdentityDbContext
    {
        public BlazorServerSignalRDbContext(DbContextOptions<BlazorServerSignalRDbContext> options)
            : base(options)
        {
        }
    }
}