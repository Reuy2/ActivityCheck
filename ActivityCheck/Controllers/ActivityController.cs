using ActivityCheck.DAL.Interfaces;
using ActivityCheck.Service.Interfaces;
using ActivityCheck.Domain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace ActivityCheck.Controllers
{
    public class ActivityController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IActivityService _activityService;

        public ActivityController(ILogger<HomeController> logger, IActivityService activityService)
        {
            _logger = logger;
            _activityService = activityService;
        }
        public async Task<IActionResult> GetActivities()
        {
            var response =await  _activityService.GetActivities();
            if(response.StatusCode == Domain.Enum.StatusCode.Ok)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }
    }
}
