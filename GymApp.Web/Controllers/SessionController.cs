using GymApp.BLL.Contracts;
using GymApp.BLL.ViewModels.Sessions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymApp.Web.Controllers
{
    public class SessionsController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly ITrainerService _trainerService;
        public SessionsController(ISessionService sessionService, ITrainerService trainerService)
        {
            _sessionService = sessionService;
            _trainerService = trainerService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var sessions = await _sessionService.GetAllSessionsAsync(cancellationToken);
            return View(sessions);
        }

        #region Create

        [HttpGet]
        public async Task<IActionResult> CreateAsync(CancellationToken cancellationToken)
        {
            await PopulateDropDownAsync(cancellationToken);
            return View();
        }

        

        [HttpPost]
        public async Task<IActionResult> Create(CreateSessionViewModel session, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropDownAsync(cancellationToken);
                return View(session);
            }
                
            var result = await _sessionService.CreateSessionAsync(session, cancellationToken);
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = "Session created successfully.";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = result.Error;
            await PopulateDropDownAsync(cancellationToken);
            return View(session);
        }

        private async Task PopulateDropDownAsync(CancellationToken cancellationToken)
        {
            var trainers = await _trainerService.GetTrainersAsync(cancellationToken);
            var categories = await _sessionService.GetCategorySelectItemsAsync(cancellationToken);

            SelectList TrainerList = new SelectList(trainers, "Id", "Name");
            SelectList CategoryList = new SelectList(categories, "Id", "Name");

            ViewBag.TrainerList = TrainerList;
            ViewBag.CategoryList = CategoryList;
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
        {
            await PopulateDropDownAsync(cancellationToken);
            var result = await _sessionService.GetSessionByIdAsync(id, cancellationToken);
            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Error;
                return RedirectToAction(nameof(Index));
            }
            return View(result.Data);
        }

        #region Edit
        [HttpGet]

        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            await PopulateDropDownAsync(cancellationToken);
            var result = await _sessionService.GetSessionToUbdateAsync(id, cancellationToken);
            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Error;
                return RedirectToAction(nameof(Index));
            }
            return View(result.Data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, UpdateSessionViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropDownAsync(cancellationToken);
                return View(model);
            }

            var result = await _sessionService.UpdateSessionAsync(id, model, cancellationToken);
            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = "Session updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = result.Error;
            await PopulateDropDownAsync(cancellationToken);
            return View(model);
        }
        #endregion

        // delete member
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _sessionService.DeleteSessionAsync(id, cancellationToken);
            if (result)
                TempData["SuccessMessage"] = "Session delete";
            else
                TempData["ErrorMessage"] = "Can't Delete the session";
            return RedirectToAction(nameof(Index));
        }
    }
}

