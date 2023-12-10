using ActivityCheck.DAL.Interfaces;
using ActivityCheck.Service.Interfaces;
using ActivityCheck.Domain.Enum;
using Microsoft.AspNetCore.Mvc;
using ActivityCheck.Domain.ViewEntity.Activity;
using ActivityCheck.Domain.Response;
using ActivityCheck.Domain.Entity;
using System.ComponentModel;

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

        [HttpGet]
        public async Task<IActionResult> GetActivities()
        {
                return View();
        }

        [HttpPost]
        public async Task<List<object>> GetAllActivitiesForChart()
        {
            List<object> data = new List<object>();

            List<string> labels;
            List<int> labelsData;

            var resp = await _activityService.GetActivities();
            if(resp.StatusCode == Domain.Enum.StatusCode.Ok)
            {
                labels = resp.Data.Select(p=>p.Created.ToShortDateString()).ToList();
                labelsData = resp.Data.Select(p => p.DurationInSec).ToList();
                data.Add(labels);
                data.Add(labelsData);
                return data;
            }
            data.Add(null);
            data.Add(null);
            return data;
        }
        [HttpGet]
        public async Task<IActionResult> GetActivitiesByDate([FromQuery] string date)
        {
            DateTime.Parse(date);
            var datetime = DateTime.Parse(date);
            var response = await _activityService.GetActivitiesByDate(datetime);
            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        [HttpGet]
        public async Task<IActionResult> Save([FromQuery] int id)
        {
            if(id == 0)
            {
                return View();
            }

            var response = await _activityService.GetActivity(id);
            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        [HttpPost]
        public async Task<IActionResult> Save(ActivityViewEntity activity)
        {
            if (ModelState.IsValid)
            {
                if(activity.id == 0)
                {
                    await _activityService.CreateActivity(activity);
                }
                else if(activity.id > 0)
                {
                    await _activityService.EditActivity(activity.id, activity);
                }
            }
            return RedirectToAction("GetActivities");
        }

        public async Task<IActionResult> DeleteActivity([FromQuery] int id)
        {
            var response = await _activityService.DeleteActivity(id);
            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
            {
                RedirectToAction("GetActivities");
            }
            return RedirectToAction("Error");
        }
    }
}
