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

        private readonly ApplicationDbContext _db ;

        public ActivityRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Activity entity)
        {
            try
            {
                await _db.AddAsync(entity);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //Логгировать ошибку
                return false;
            }
            return true;
        }

        public async Task<bool> Delete(Activity entity)
        {
            try
            {
                _db.Remove(entity);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //Логгировать ошибку
                return false;
            }
            return true;
        }

        public async Task<Activity> Get(int id)
        {
            return await _db.Activity.FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task<IEnumerable<Activity>> GetAll()
        {
            return await _db.Activity.ToListAsync();
        }

        public async Task<Activity> GetByName(string name)
        {
            return await _db.Activity.FirstOrDefaultAsync(obj => obj.Name == name);
        }
    }
}
