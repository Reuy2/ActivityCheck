using ActivityCheck.DAL.Interfaces;
using ActivityCheck.Domain.Entity;
using ActivityCheck.Domain.Response;
using ActivityCheck.Domain.ViewEntity.Activity;

namespace ActivityCheck.Service.Interfaces
{
    public interface IActivityService
    {
        public Task<BaseResponse<IEnumerable<Activity>>> GetActivities();
        public Task<BaseResponse<IEnumerable<Activity>>> GetActivitiesByDate(DateTime date);

        public Task<BaseResponse<Activity>> GetActivity(int id);
        public Task<BaseResponse<Activity>> CreateActivity(ActivityViewModel activity);
        public Task<BaseResponse<bool>> DeleteActivity(int id);
        public Task<BaseResponse<Activity>> EditActivity(int id, ActivityViewModel activity);
    }
}
