using WerfLogDal.Models;

namespace WerfLogDal.Interfaces
{
    public interface ITijdregistratieRepository : IGenericRepository<Tijdregistratie>
    {
        Task<Tijdregistratie> GetEmptyStopDateTimeAsync();
        Task UpdateStopTijdById(int id, DateTime stopTijd);

        Task<Tijdregistratie> GetTijdregistratieById(int id);

        Task<int> HaalTotaalUrenOpPerMaand(int jaar, int maand);

        Task<List<Tijdregistratie>> HaalAlleTijdregistratiesOpPerMaand(int jaar, int maand);

        Task UpdateTijdregistratieById(int id, Tijdregistratie tijdregistratie);
    }
}
