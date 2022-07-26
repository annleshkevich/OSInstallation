using Microsoft.EntityFrameworkCore;

namespace OS_Installation.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options) {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        public DbSet<OS> OperatingSystems { get; set; } = null!;
        public DbSet<Computer> Computers { get; set; } = null!;
        public DbSet<Installer> Installers { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //cascade removal
            modelBuilder.Entity<Computer>()
                .HasOne(p => p.OS)
                .WithMany(c => c.Computers)
                .OnDelete(DeleteBehavior.Cascade);
        }
        }
}
