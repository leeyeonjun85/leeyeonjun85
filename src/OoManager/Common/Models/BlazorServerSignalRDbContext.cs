using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OoManager.Models
{
    public class BlazorServerSignalRDbContext : IdentityDbContext
    {
        public BlazorServerSignalRDbContext(DbContextOptions<BlazorServerSignalRDbContext> options)
            : base(options)
        {
        }
    }
}