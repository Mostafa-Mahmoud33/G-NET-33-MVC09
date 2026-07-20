namespace GymApp.DAl.Context.Configurations
{
    internal abstract class GymUserConfiguration<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.Name)
                       .HasColumnType("varchar")
                       .HasMaxLength(50);

            builder.Property(x => x.Email)
                       .HasColumnType("varchar")
                       .HasMaxLength(100);

            builder.OwnsOne(x => x.Address, address =>
                {
                    address.Property(a => a.Street)
                           .HasColumnType("varchar")
                           .HasMaxLength(100);


                    address.Property(a => a.City)
                                   .HasColumnType("varchar")
                                   .HasMaxLength(50);
                });

            builder.Property(x => x.Phone)
                   .HasColumnType("char")
                   .HasMaxLength(15);


            // Check Constraint for Email Address & Phone 

            builder.ToTable(x =>
                    {
                        x.HasCheckConstraint("EmailCheck", "Email LIKE '%@%.%'");
                        x.HasCheckConstraint("PhoneCheck", "Phone LIKE '[0-9]%' AND LEN(Phone) = 11");
                    });
        }
    }
}
