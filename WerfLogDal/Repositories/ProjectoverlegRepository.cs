using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
