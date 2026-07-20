namespace GymApp.DAl.Context.Configurations
{
    internal class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable(x =>
            {
                x.HasCheckConstraint("SessionDatesCheck", "StartDate < EndDate");

                x.HasCheckConstraint("SessionCapacityCheck", "Capacity BETWEEN 1 AND 25");
            });
        }
    }
}
