using WerfLogBl.DTOS;

namespace WerfLogBl.Interfaces
{
    public interface INotitieManager
    {
        //NotitieDto AddNotitie(NotitieDto notitieDto);

        Task<NotitieDto> AddNotitieAsync(NotitieDto notitieDto);

        Task<List<NotitieDto>> GetAllNotities(int werfId);

        Task DeleteNotitieAsync(NotitieDto notitieDto);
    }
}
