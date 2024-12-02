using HouseFinderAPI.Data;
using HouseFinderAPI.Models;
using HouseFinderAPI.Repository.IRepository;

namespace HouseFinderAPI.Repository
{
    public class HouseRepository : BaseRepository<House>, IHouseRepository
    {
        private readonly ApplicationDbContext context;
        public HouseRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

    }
}
