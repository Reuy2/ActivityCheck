using ActivityCheck.DAL.Interfaces;
using ActivityCheck.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityCheck.DAL.Repositories
{
    public class ActivityRepository : IActivityRepository
    {

        private readonly ApplicationDbContext _db;

        public ActivityRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool Create(Activity entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Activity entity)
        {
            throw new NotImplementedException();
        }

        public Activity Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Activity>> GetAll()
        {
            return await _db.Activity.ToListAsync();
        }

        public Activity GetByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
