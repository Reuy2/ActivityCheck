using ActivityCheck.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityCheck.DAL.Interfaces
{
    public interface IActivityRepository : IBaseRepository<Activity>
    {
        Task<Activity> GetByName(string name);
    }
}
