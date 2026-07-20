

namespace GymApp.BLL.Services
{
    public class MemberService : IMemberService
    {

        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAttachmentService _attachmentService;
        public MemberService(IUnitOfWork unitOfWork, IMapper mapper, IAttachmentService attachmentService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _attachmentService = attachmentService;
        }

        public async Task<bool> CreateMemberAsync(CreateMemberViewModel createMemberViewModel, CancellationToken cancellationToken = default)
        {
            var _memberRepo = _unitOfWork.GetRepository<Member>();  // Create Member Repo

            var emailExists = await _memberRepo.AnyAsync(m => m.Email == createMemberViewModel.Email, cancellationToken);
            var phoneExists = await _memberRepo.AnyAsync(m => m.Phone == createMemberViewModel.Phone, cancellationToken);

            if (emailExists || phoneExists) 
                return false;
            

            // TODO : uplaod the Image + Add path to member


            var member = _mapper.Map<CreateMemberViewModel , Member>(createMemberViewModel);
            if (createMemberViewModel.Photo is not null)
            {
                member.Photo = await _attachmentService.UploadAsync(createMemberViewModel.Photo.OpenReadStream(),
                    createMemberViewModel.Photo.FileName, "MembersPhotos", cancellationToken) ?? string.Empty;
            }
            _memberRepo.Add(member, cancellationToken: cancellationToken);

            
            var result = (await _unitOfWork.SaveChangesAsync(cancellationToken)) > 0;

            if(!result)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteMemberAsync(int memberId, CancellationToken cancellationToken = default)
        {
            var _memberRepo = _unitOfWork.GetRepository<Member>();
            var member = await _memberRepo.GetByIdAsync(memberId, cancellationToken);

            if (member == null)
                return false;

            // check if there is active bookings

            var hasActiveSession = await _unitOfWork.GetRepository<Booking>().AnyAsync(x => x.MemberId == memberId && x.Session.StartDate > DateTime.Now, cancellationToken);

            if (hasActiveSession)
                return false;
            _memberRepo.Delete(member, cancellationToken);
            var result = (await _unitOfWork.SaveChangesAsync(cancellationToken)) > 0;

            if (result)
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<MemberViewModels>> GetAllMembersAsync(CancellationToken cancellationToken = default)
        {
            // Call DB => MemberRepo => GenericRepo<Member>

            // MemberRepo => Get All
            var _memberRepo = _unitOfWork.GetRepository<Member>();
            var members = await _memberRepo.GetAllAsync(cancellationToken: cancellationToken);

            // Mapping => Member => MemberViewModels
           
            return _mapper.Map<IEnumerable<Member>, IEnumerable<MemberViewModels>>(members);
        }

        public async Task<HealthRecordViewModel?> GetHealthRecordDetailsViewModelAsync(int memberId, CancellationToken cancellationToken)
        {
            var record = await _unitOfWork.GetRepository<HealthRecord>().FirstOrDefault(x => x.MemberId == memberId, cancellationToken);
            if (record == null)
                return null;

            return new HealthRecordViewModel
            {
                Height = record.Height,
                Weight = record.Weight,
                BloodType = record.BloodType,
                Note = record.Note
            };
        }

        public async Task<MemberEditViewModel?> GetMemberDetailForEditAsync(int id, CancellationToken cancellationToken = default)
        {
            var member = await _unitOfWork.GetRepository<Member>().GetByIdAsync(id, cancellationToken: cancellationToken);
            if (member is null)
                return null;

            return new MemberEditViewModel
            {
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                BuildingNumber = member.Address.BuildingNumber,
                Street = member.Address.Street,
                City = member.Address.City
            };
        }

        public async Task<MemberDetailsViewModel?> GetMemberDetailsViewModelAsync(int id, CancellationToken cancellationToken = default)
        {
            // Call DB => MemberRepo => GenericRepo<Member>

            // Find Member By Id => GetByIdAsync(id)
            var _memberRepo = _unitOfWork.GetRepository<Member>();
            //var member = await _memberRepo.GetByIdAsync(id, cancellationToken: cancellationToken);
            //if(member is null)
            //    return null;

            // mapping => Member => MemberDetailsViewModel

            //var memberDetailsViewModel = _mapper.Map<Member, MemberDetailsViewModel>(member);

            var memberDetailsViewModel = _mapper.ProjectTo<MemberDetailsViewModel>(
                _unitOfWork.GetRepository<Member>().GetQueryable()).FirstOrDefault(x => x.Id == id);
            // membership 
            var membership = await _unitOfWork.GetRepository<Membership>().FirstOrDefault(x => x.MemberId == memberDetailsViewModel.Id && x.EndDate > DateOnly.FromDateTime(DateTime.Now));

            if (membership is not null)
            { 
                var plan = await _unitOfWork.GetRepository<Plan>().GetByIdAsync(membership.PlanId, cancellationToken: cancellationToken);

                memberDetailsViewModel.MembershipEndDate = membership.EndDate.ToShortDateString();
                memberDetailsViewModel.MembershipEndDate = membership.CreatedAt.ToShortDateString();
                memberDetailsViewModel.PlanName = plan?.Name ?? "No Plan";
            }


            return memberDetailsViewModel;
        }

        public async Task<bool> UpdateMemberAsync(int id, MemberEditViewModel memberViewModel, CancellationToken cancellationToken)
        {
            var _memberRepo = _unitOfWork.GetRepository<Member>();
            var member = await _unitOfWork.GetRepository<Member>().GetByIdAsync(id, cancellationToken: cancellationToken);
            if (member is null)
                return false;

            var emailExists = await _unitOfWork.GetRepository<Member>().AnyAsync(m => m.Email == memberViewModel.Email&& m.Id != id, cancellationToken);
            var phoneExists = await _unitOfWork.GetRepository<Member>().AnyAsync(m => m.Phone == memberViewModel.Phone && m.Id != id, cancellationToken);

            if (emailExists || phoneExists)
                return false;

            // Update the member from the view model
            member.Name = memberViewModel.Name;

            member.Email = memberViewModel.Email;
            member.Phone = memberViewModel.Phone;


            
            member.Address.BuildingNumber = memberViewModel.BuildingNumber;
            member.Address.Street = memberViewModel.Street;
            member.Address.City = memberViewModel.City;

            _memberRepo.Update(member, cancellationToken : cancellationToken);
            return (await _unitOfWork.SaveChangesAsync(cancellationToken)) >0; //Save changes to the database

            
        }

    }
}
