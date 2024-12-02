using HouseFinderAPI.Data;
using HouseFinderAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;

namespace HouseFinderAPI.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext context;
        internal DbSet<T> dbSet;
        public BaseRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public async Task Create(T house)
        {
            await dbSet.AddAsync(house);
            await Save();
        }

        public async Task<T> Get(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dbSet;
            if (query != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }
        public async Task<List<T>> GetAll()
        {
            IQueryable<T> query = dbSet;
            return await query.ToListAsync();
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = dbSet;
            if (query != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task Remove(T house)
        {
            dbSet.Remove(house);
            await Save();
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
