using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OoManager.Common
{
    public class BlazorServerSignalRDbContext : IdentityDbContext
    {
        public BlazorServerSignalRDbContext(DbContextOptions<BlazorServerSignalRDbContext> options)
            : base(options)
        {
        }
    }
}