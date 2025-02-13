using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Data;

public partial class ApplicationDbContext : DbContext
{
  
    public ApplicationDbContext()
    {
    }

  
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<Intern> Interns { get; set; }
    public virtual DbSet<AllowAccess> AllowAccesses { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)    
            .WithOne(r => r.User)    
            .HasForeignKey<User>(u => u.RoleId);  

        modelBuilder.Entity<AllowAccess>()
            .HasOne(a => a.Role) 
            .WithMany(r => r.AllowAccesses)  
            .HasForeignKey(a => a.RoleId);  
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Demo;Username=postgres;Password=123456");
        }
    }
}
