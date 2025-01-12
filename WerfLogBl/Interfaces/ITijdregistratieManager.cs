﻿using WerfLogBl.DTOS;

namespace WerfLogBl.Interfaces
{
    public interface ITijdregistratieManager
    {
        Task<int> VoegStarttijdWerfToe(WerfDto werf);
        Task VoegStoptijdWerfToe(int actieveTijdId);
        Task<TijdregistratieDto> GetActieveTijdregistratieId();
        Task<int> GetTotaalTijdRegistratiesMaand(int maand, int jaar);
        Task<List<TijdregistratieDto>> GetAlleTijdRegistratiesMaand(int maand, int jaar);

        Task<TijdregistratieDto> GetTijdregistratieById(int id);

        Task UpdateTijdregistratie(int id, TijdregistratieDto tijdregistratieDto);

        Task DeleteTijdregistratieAsync(TijdregistratieDto tijdregistratieDto);
    }
}
