using WerfLogDal.Models;

namespace WerfLogDal.Interfaces
{
    public interface INotitieRepository : IGenericRepository<Notitie>
    {
        Task<List<Notitie>> GetAllNotitiesByWerfIdAsync(int Id);
    }



}
