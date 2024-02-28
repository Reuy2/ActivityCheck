using ActivityCheck.DAL.Interfaces;
using ActivityCheck.Service.Interfaces;
using ActivityCheck.Domain.Enum;
using Microsoft.AspNetCore.Mvc;
using ActivityCheck.Domain.ViewEntity.Activity;
using ActivityCheck.Domain.Response;
using ActivityCheck.Domain.Entity;
using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ActivityCheck.Domain.Chart;

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
        [Authorize]
        [HttpGet]
        public IActionResult GetActivities()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<List<object>> GetAllActivitiesForChart()
        {
            List<object> data = new List<object>();

            List<string> labels = new List<string>();
            List<BarChartObj> labelsData = new List<BarChartObj>();

            var resp = await _activityService.GetActivities(long.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value));

            if (resp.StatusCode == Domain.Enum.StatusCode.Ok)
            {
                labels = resp.Data.Select(p => p.Created.ToShortDateString()).Distinct().ToList();
                var activityList = resp.Data.ToList();

                var activityNames = resp.Data.Select(x => x.Name).ToList();
                for (int i = 0; i < resp.Data.Count(); i++)
                {
                    List<int> Data = new List<int>();
                    for (int j = 0; j < labels.Count(); j++)
                    {
                        var activity = activityList.FirstOrDefault(x => x.Created.ToShortDateString().Equals(labels[j]) && x.Name == activityNames[i]);
                        if (activity is null)
                        {
                            Data.Add(0);
                            continue;
                        }
                        Data.Add((int)activity.DurationInSec);
                        activityList.Remove(activity);
                    }
                    labelsData.Add(new BarChartObj()
                    {
                        label = activityNames[i],
                        data = Data,
                        backgroundColor = ChartColors.Colors[i % resp.Data.Count()],
                    });
                }
                data.Add(labels);
                data.Add(labelsData);
                return data;
            }
            return data;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetActivitiesByDate([FromQuery] string date)
        {
            if (String.IsNullOrEmpty(date))
            {
                RedirectToAction("Error");
            }
            var datetime = DateTime.Parse(date);
            long id = long.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var response = await _activityService.GetActivitiesByDate(datetime, id);
            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        [HttpGet]
        public async Task<IActionResult> Save([FromQuery] int id)
        {
            if (id == 0)
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

        [HttpGet]
        public async Task<IActionResult> Save(ActivityViewModel activity)
        {
            if (ModelState.IsValid)
            {
                if (activity.id == 0)
                {
                    await _activityService.CreateActivity(activity);
                }
                else if (activity.id > 0)
                {
                    await _activityService.EditActivity(activity.id, activity);
                }
            }
            return RedirectToAction("GetActivities");
        }
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteActivity([FromBody] int id)
        {
            var response = await _activityService.DeleteActivity(id);
            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
            {
                return new OkResult();
            }
            return new StatusCodeResult((int)response.StatusCode);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateActivity([FromBody] ActivityViewModel activityView)
        {
            if(activityView is null)
            {
                return new BadRequestResult();
            }

            long userId = long.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            var response = await _activityService.CreateActivity(activityView, userId);

            if(response.StatusCode == Domain.Enum.StatusCode.Ok)
            {
                return new OkObjectResult(response);
            }

            return Redirect("/Error");
        }
    }
}
