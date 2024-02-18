using WerfLogBl.DTOS;

namespace WerfLogBl.Interfaces
{
    public interface IWerfManager
    {
        Task<List<WerfDto>> HaalAlleWervenOp();
        Task<WerfDto> AddWerfAsync(WerfDto werfDto);

        Task DeleteWerfAsync(WerfDto werfDto);
    }
}
