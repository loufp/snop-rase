using Microsoft.EntityFrameworkCore;
using snoperase.Domain.Entites;

namespace snoperase.Infastrucure.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(e =>
        {
            e.ToTable("Users");
            e.HasKey(x => x.Id);

            e.Property(x => x.Username).HasMaxLength(100).IsRequired();
            e.Property(x => x.Email).HasMaxLength(256).IsRequired();
            e.HasIndex(x => x.Email).IsUnique();

            e.Property(x => x.Password).HasMaxLength(60).IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }
}