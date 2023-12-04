using ActivityCheck.DAL.Interfaces;
using ActivityCheck.Domain.Entity;
using ActivityCheck.Domain.Enum;
using ActivityCheck.Domain.Response;
using ActivityCheck.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityCheck.Service.Implementations
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;

        public ActivityService(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public async Task<BaseResponse<IEnumerable<Activity>>> GetActivities()
        {
            var response = new BaseResponse<IEnumerable<Activity>>();
            try
            {
                var activities = await _activityRepository.GetAll();
                if(activities.Count() == 0)
                {
                    response.Description = "Не найдено обьектов";
                    response.StatusCode = StatusCode.NotFound;
                    return response;
                }

                response.StatusCode = StatusCode.Ok;
                response.Data = activities;
                return response;
            }
            catch (Exception ex)
            {
                response.Description = $"[GetActivities] : {ex.Message}";
                response.StatusCode = StatusCode.InternalServerError;
                return response;
            }
        }
    }
}
