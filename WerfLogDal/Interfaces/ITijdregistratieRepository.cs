using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WerfLogDal.Models;

namespace WerfLogDal.Interfaces
{
    public interface ITijdregistratieRepository : IGenericRepository<Tijdregistratie>
    {
        Task<Tijdregistratie> GetEmptyStopDateTimeAsync();
        Task UpdateStopTijdById(int id, DateTime stopTijd);

        Task<Tijdregistratie> GetTijdregistratieById(int id);
    }
}
