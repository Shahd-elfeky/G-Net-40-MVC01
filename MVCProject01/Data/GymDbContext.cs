using Microsoft.EntityFrameworkCore;
using MVCProject01.Models;

namespace MVCProject01.Data;

public class GymDbContext : DbContext
{
    public GymDbContext(DbContextOptions<GymDbContext> options)
        : base(options)
    {
    }

    public DbSet<Plan> Plans => Set<Plan>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Plan>(entity =>
        {
            entity.Property(plan => plan.Price)
                .HasPrecision(18, 2);

            entity.Property(plan => plan.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
        });
    }
}
