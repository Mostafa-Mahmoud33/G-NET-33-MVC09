using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymApp.BLL.Contracts;

namespace GymApp.Web.Controllers
{
    public class PlansController : Controller
    {
        private readonly IPlansService _service;

        public PlansController(IPlansService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var plans = await _service.PlansAsync(cancellationToken);
            return View(plans);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
        {
            var plan = await _service.GetPlanByIdAsync(id, cancellationToken);
            if (plan == null)
                return RedirectToAction(nameof(Index));

            return View(plan);
        }
    }
}
