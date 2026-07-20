
namespace GymApp.DAl.Context.Configurations
{
    internal class MemberConfiguration
        : GymUserConfiguration<Member>, IEntityTypeConfiguration<Member>
    {
        public new void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.Property(x => x.Gender)
                   .HasConversion<string>();

            base.Configure(builder);
        }
    }
}