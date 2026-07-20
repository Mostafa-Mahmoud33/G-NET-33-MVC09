using GymApp.BLL.Contracts;
using GymApp.BLL.ViewModels.Member;
using Microsoft.AspNetCore.Mvc;

namespace GymApp.Web.Controllers
{
    public class MembersController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly IAttachmentService _attachmentService;

        public MembersController(IMemberService memberService, IAttachmentService attachmentService)
        {
            _memberService = memberService;
            _attachmentService = attachmentService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            // logic to retrieve members from the database would go here
            //ViewBag.Message = "Hello From ViewBag";
            //ViewData["Data"] = new MemberViewModels();
            var members = await _memberService.GetAllMembersAsync(cancellationToken); 
            return View(members);
        }
        #region Create
        [HttpGet]

        public IActionResult Create(CreateMemberViewModel memberViewModel, CancellationToken cancellationToken)
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMemberViewModel memberViewModel)
        {
            //Server Side Validation

            if(!ModelState.IsValid)
                return View(memberViewModel);

            // Call Service to Create Member
            var result = await _memberService.CreateMemberAsync(memberViewModel);
             // if member is created successfully, redirect to the members list
             if(result)
                TempData ["SuccessMessage"] = "Member Created Successfully";
            else
                TempData ["ErrorMessage"] = "Member Creation Failed";

            return RedirectToAction(nameof(Index));
           
        }
        #endregion


        // Details

        // Members Details
        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
        {
            // logic to retrieve member details from the database would go here

            var memberDetails = await _memberService.GetMemberDetailsViewModelAsync(id);
            if (memberDetails is not null)
                return View(memberDetails);

            return NotFound();
        }

        // Health Record Details
        [HttpGet]
        public async Task<IActionResult> HealthRecordDetails(int id, CancellationToken cancellationToken)
        {

            var healthRecordDetails = await _memberService.GetHealthRecordDetailsViewModelAsync(id, cancellationToken);
            if (healthRecordDetails is not null)
                return View(healthRecordDetails);

            return NotFound();
        }


        // Edit Member
        [HttpGet]

        // Get The Member Details to Edit And return the View with the Member editable Details
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var memberDetails = await _memberService.GetMemberDetailForEditAsync(id, cancellationToken);
            if (memberDetails is not null)
                return View(memberDetails);
            return NotFound();
        }

        // Post The Edited Member Details to the Update the Member in the Database 
        [HttpPost]
        public async Task<IActionResult> Edit(int id, MemberEditViewModel memberViewModel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return View(memberViewModel);

            var result = await _memberService.UpdateMemberAsync(id, memberViewModel, cancellationToken);
            if (result)
                TempData["SuccessMessage"] = "Member Updated Successfully";
            else
                TempData["ErrorMessage"] = "Member Update Failed";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _memberService.DeleteMemberAsync(id, cancellationToken);

            if (result)
                TempData["SuccessMessage"] = "Member deleted";
            else
                TempData["ErrorMassage"] = "Can't Delete the member";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Picture(int id, CancellationToken cancellationToken)
        {
            var member = await _memberService.GetMemberDetailsViewModelAsync(id, cancellationToken );
            if (member is null)
                return NotFound();
            var result = await _attachmentService.GetFileAsync(member.Photo, "MembersPhotos", cancellationToken);
            if (!result.HasValue)
                return NotFound();
            return File(result.Value.stream, result.Value.contentType);
        }
    }

}
