using WerfLogDal.Interfaces;
using WerfLogDal.Models;

namespace WerfLogDal.Repositories
{
    public class ProjectoverlegRepository : GenericRepository<ProjectOverleg>, IProjectOverlegRepository
    {
        public ProjectoverlegRepository(DbContext connection) : base(connection)
        {
        }
    }
}
