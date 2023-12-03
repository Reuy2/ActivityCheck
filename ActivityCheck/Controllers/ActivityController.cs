using ActivityCheck.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ActivityCheck.Controllers
{
    public class ActivityController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IActivityRepository _activityRepository;

        public ActivityController(ILogger<HomeController> logger, IActivityRepository activityRepository)
        {
            _logger = logger;
            _activityRepository = activityRepository;
        }
        public async Task<IActionResult> GetActivities()
        {
            var response = await _activityRepository.GetAll();
            return View(response);
        }
    }
}
