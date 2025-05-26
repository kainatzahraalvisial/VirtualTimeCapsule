using Microsoft.EntityFrameworkCore;
using Models;
using time_capsule.Models;

namespace time_capsule.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Capsule> Capsules { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<Notifications> Notifications { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("User", "dbo");
            modelBuilder.Entity<Capsule>().ToTable("Capsules", "dbo");
            modelBuilder.Entity<ContactMessage>().ToTable("ContactMessages", "dbo");
            modelBuilder.Entity<Notifications>().ToTable("Notifications", "dbo");

            modelBuilder.Entity<Capsule>()
                .Property(c => c.Id).HasColumnName("Id");
            modelBuilder.Entity<Capsule>()
                .Property(c => c.Title).HasColumnName("Title");
            modelBuilder.Entity<Capsule>()
                .Property(c => c.Content).HasColumnName("Content");
            modelBuilder.Entity<Capsule>()
                .Property(c => c.CreatedDate).HasColumnName("CreatedDate");
            modelBuilder.Entity<Capsule>()
                .Property(c => c.OpenDate).HasColumnName("OpenDate");
            modelBuilder.Entity<Capsule>()
                .Property(c => c.ImagePath).HasColumnName("ImagePath");
            modelBuilder.Entity<Capsule>()
                .Property(c => c.UserId).HasColumnName("UserId");

            modelBuilder.Entity<Capsule>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Notifications>()
                .HasOne(n => n.User)
                .WithMany()
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Notifications>()
                .HasOne(n => n.Capsule)
                .WithMany(c => c.Notifications)
                .HasForeignKey(n => n.CapsuleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}