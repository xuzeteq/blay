   using Microsoft.EntityFrameworkCore;
using Modules.Users.Domain;

namespace Modules.Users.Infrastructure
{
    public class UsersDbContext(DbContextOptions<UsersDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("users");

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.HasIndex(e => e.TwitchId);
                entity.Property(e => e.TwitchId)
                    .HasColumnName("twitch_id")
                    .IsRequired();

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.AvatarUrl)
                    .HasColumnName("avatar_url")
                    .IsRequired()
                    .HasMaxLength(200);
            });
        }
    }
}
