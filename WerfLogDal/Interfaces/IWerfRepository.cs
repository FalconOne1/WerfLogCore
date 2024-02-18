using WerfLogDal.Models;

namespace WerfLogDal.Interfaces
{
    public interface IWerfRepository : IGenericRepository<Werf>
    {
        Task<List<Werf>> GetAllWervenAsync();
    }
}
