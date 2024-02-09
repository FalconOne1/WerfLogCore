using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WerfLogDal.Models;

namespace WerfLogDal.Interfaces
{
    public interface IWerfRepository : IGenericRepository<Werf>
    {
        Task<List<Werf>> GetAllWervenAsync();
    }
}
