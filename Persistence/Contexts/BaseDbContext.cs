using Domain.BaseProjeEntities.FileEntities;
using Domain.BaseProjeEntities.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using File = Domain.BaseProjeEntities.FileEntities.File;

namespace Persistence.Contexts;

public class BaseDbContext : IdentityDbContext<AppUser,AppRole,Guid>
{
    public BaseDbContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<File> Files { get; set; }
    public DbSet<ImageFile> ProductImageFiles { get; set; }
    public DbSet<InvoiceFile> InvoiceFiles { get; set; }
    public DbSet<Menu> Menus { get; set; }
    public DbSet<Endpoint> Endpoints { get; set; }
}
