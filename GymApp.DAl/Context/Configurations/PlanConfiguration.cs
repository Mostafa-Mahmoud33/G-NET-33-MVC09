
namespace GymApp.DAl.Context.Configurations
{
    public class PlanConfiguration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.Property(x => x.Name)
                   .HasColumnType("varchar")
                   .HasMaxLength(50);

            builder.Property(x => x.Description)
                   .HasMaxLength(200);

            builder.Property(x => x.Price)
                   .HasPrecision(10, 2);

            builder.Property(x => x.CreatedAt)
                   .HasDefaultValueSql("GETDATE()");

            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint(
                    "PlanDurationCheck",
                    "DurationDays BETWEEN 1 AND 365");
            });

            builder.HasData(
                new Plan { Id = 1, Name = "Basic Plan", DurationDays = 30, Price = 200m, Description = "Gym access during staffed hours",
                    IsActive = true, CreatedAt = new DateTime(2025, 1, 1) },
                new Plan { Id = 2, Name = "Standard Plan", DurationDays = 60, Price = 350m, Description = "Full gym access Including locker room",
                    IsActive = true, CreatedAt = new DateTime(2025, 1, 1) },
                new Plan {Id = 3,Name = "Premium Plan", DurationDays = 90, Price = 500m, Description = "Full access Plus one personal trainer session",
                    IsActive = true, CreatedAt = new DateTime(2025, 1, 1) },
                new Plan { Id = 4,  Name = "Annual Plan",  DurationDays = 365, Price = 1500m, Description = "Unlimited yearly gym access",
                    IsActive = true, CreatedAt = new DateTime(2025, 1, 1) }
            );
        }
    }
}