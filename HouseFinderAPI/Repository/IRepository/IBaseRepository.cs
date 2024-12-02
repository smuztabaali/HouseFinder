using System.Linq.Expressions;

namespace HouseFinderAPI.Repository.IRepository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<List<T>> GetAll(Expression<Func<T, bool>> filter = null);
        Task<T> Get(Expression<Func<T, bool>>? filter = null);
        Task Create(T house);
        Task Remove(T house);
        Task Save();
    }
}
