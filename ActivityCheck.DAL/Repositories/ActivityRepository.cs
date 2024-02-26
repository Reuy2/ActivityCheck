using ActivityCheck.DAL.Interfaces;
using ActivityCheck.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using Activity = ActivityCheck.Domain.Entity.Activity;

namespace ActivityCheck.DAL.Repositories
{
    public class ActivityRepository : IBaseRepository<Activity>
    {

        private readonly ApplicationDbContext _db ;

        public ActivityRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(Activity entity)
        { 
            await _db.Activity.AddAsync(entity);
            await _db.SaveChangesAsync();

        }

        public async Task Delete(Activity entity)
        {
            _db.Activity.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Activity> GetAll()
        {
            return _db.Activity;
        }

        public async Task<Activity> Update(Activity entity)
        {

            _db.Activity.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
