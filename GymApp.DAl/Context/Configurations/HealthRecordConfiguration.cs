namespace GymApp.DAl.Context.Configurations
{
    internal class HealthRecordConfiguration : IEntityTypeConfiguration<HealthRecord>
    {
        public void Configure(EntityTypeBuilder<HealthRecord> builder)
        {
            builder.Property(hr => hr.Height)
                   .HasColumnType("decimal(5, 2)")
                   .IsRequired();

            builder.Property(hr => hr.Weight)
                   .HasColumnType("decimal(5, 2)")
                   .IsRequired();

            builder.Property(hr => hr.BloodType)
                .HasColumnType("char")
                .HasColumnType("varchar(5)");

            builder.Property(hr => hr.Note)
                   .HasMaxLength(200);
                   

        }
    }
}
