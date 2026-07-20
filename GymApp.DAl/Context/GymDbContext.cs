using Microsoft.AspNetCore.Identity;
using GymApp.DAl.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GymApp.DAl.Context
{
    public class GymDbContext : IdentityDbContext<ApplicationUser>
    {

        public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
        {
        }


       // public DbSet<ApplicationUser> Users { get; set; }

        public DbSet<Plan> Plans { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<HealthRecord> HealthRecords { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Membership> Memberships { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GymDbContext).Assembly);

            modelBuilder.Entity<ApplicationUser>(b =>
            {
                b.Property(u => u.FirstName)
                .HasColumnType("VarChar")
                .HasMaxLength(256);
                b.Property(u => u.LastName)
                .HasColumnType("VarChar")
                .HasMaxLength(256);
            });
        }
    }
}
