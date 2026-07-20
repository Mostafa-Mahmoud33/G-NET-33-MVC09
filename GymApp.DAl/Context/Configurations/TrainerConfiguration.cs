namespace GymApp.DAl.Context.Configurations
{
    internal class TrainerConfiguration : GymUserConfiguration<Trainer>, IEntityTypeConfiguration<Trainer>
    {
        public new void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.Property(x => x.Specialty)
                   .HasConversion<string>();
            
            base.Configure(builder);
        }
    }
}
