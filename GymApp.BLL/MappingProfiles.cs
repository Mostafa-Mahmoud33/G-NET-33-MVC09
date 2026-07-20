using GymApp.BLL.ViewModels.Sessions;

namespace GymApp.BLL
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMemberMaps();
            CreateMap<Session, SessionViewModel>()
                .ForMember(d => d.TrainerName, opt => opt.MapFrom(s => s.Trainer.Name))
                .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.Category.Name));

            CreateMap<CreateMemberViewModel, Session>();

            CreateMap<Category, CategorySelectItemViewModel>();

            CreateMap<CreateSessionViewModel, Session>();
            CreateMap<UpdateSessionViewModel, Session>();

            CreateMap<Session, UpdateSessionViewModel>();
        }

        private void CreateMemberMaps()
        {
            // Member => MemberViewModels

            CreateMap<Member, MemberViewModels>();


            //CreateMemberViewModel => Member
            CreateMap<CreateMemberViewModel, Member>()
                .ForMember(d => d.Address,
                o => o.MapFrom(s => new Address
                {
                    BuildingNumber = s.BuildingNumber,
                    City = s.City,
                    Street = s.Street
                }));


            CreateMap<HealthRecordViewModel, HealthRecord>();


            CreateMap<Member, MemberDetailsViewModel>()
                .ForMember(d => d.Address, o => o.MapFrom(s => $"{s.Address.BuildingNumber} {s.Address.Street}, {s.Address.City}"))
                .ForMember(d => d.DateOfBirth, o => o.MapFrom(s =>
                  s.DateOfBirth.ToShortDateString()));
        }
    }
}
