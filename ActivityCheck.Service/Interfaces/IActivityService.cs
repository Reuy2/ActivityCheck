using ActivityCheck.Domain.Entity;
using ActivityCheck.Domain.Response;

namespace ActivityCheck.Service.Interfaces
{
    public interface IActivityService
    {
        public Task<BaseResponse<IEnumerable<Activity>>> GetActivities();
    }
}
