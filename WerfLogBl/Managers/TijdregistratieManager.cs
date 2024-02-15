using AutoMapper;
using WerfLogBl.DTOS;
using WerfLogBl.Interfaces;
using WerfLogDal.Exceptions;
using WerfLogDal.Interfaces;
using WerfLogDal.Models;

namespace WerfLogBl.Managers
{
    public class TijdregistratieManager : ITijdregistratieManager
    {

        private readonly ITijdregistratieRepository _tijdRepository;
        private readonly IMapper _mapper;
        public TijdregistratieManager(ITijdregistratieRepository tijdRepository, IMapper mapper)
        {
            _mapper = mapper;
            _tijdRepository = tijdRepository;
        }

        public async Task<int> VoegStarttijdWerfToe(WerfDto werf)
        {

            try
            {
                // Controleer of werf.Id een waarde heeft.
                if (werf.Id.HasValue)
                {
                    // Haal de waarde op van werf.Id, wetende dat het niet null is.
                    int werfId = werf.Id.Value;
                    string werfNaam = werf.Naam;

                    Tijdregistratie tijdregistratie = new Tijdregistratie
                    {
                        Id = null, // Voor autoincrement in SQLite.
                        WerfId = werfId, // Gebruik de waarde van werf.Id hier.
                        StartTijd = DateTime.Now,
                        StopTijd = null,
                        WerfNaamRegistratie = werfNaam,
                        TotaleTijd = 0
                    };

                    Tijdregistratie nieuweTijdregistratie = await _tijdRepository.InsertWithReturnAsync(tijdregistratie);

                    if (tijdregistratie != null)
                    {
                        return (int)nieuweTijdregistratie.Id;
                    }
                    else
                    {
                        throw new Exception("Fout met werfID");
                    }
                }
                else
                {
                    throw new Exception("Fout met werfID");
                }
            }
            catch (DatabaseException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task VoegStoptijdWerfToe(int actieveTijdId)
        {
            try
            {
                await _tijdRepository.UpdateStopTijdById(actieveTijdId, DateTime.Now);
            }
            catch (DatabaseException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<TijdregistratieDto> GetActieveTijdregistratieId()
        {
            try
            {
                Tijdregistratie sessie = await _tijdRepository.GetEmptyStopDateTimeAsync();
                return _mapper.Map<TijdregistratieDto>(sessie);
            }
            catch (DatabaseException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> GetTotaalTijdRegistratiesMaand(int maand, int jaar)
        {

            try
            {
                int totaal = await _tijdRepository.HaalTotaalUrenOpPerMaand(maand, jaar);
                return totaal;
            }
            catch (DatabaseException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<TijdregistratieDto>> GetAlleTijdRegistratiesMaand(int maand, int jaar)
        {
            try
            {
                List<Tijdregistratie> tijdregistraties = await _tijdRepository.HaalAlleTijdregistratiesOpPerMaand(maand, jaar);
                List<TijdregistratieDto> tijdregistratiesDto = _mapper.Map<List<TijdregistratieDto>>(tijdregistraties);
                return tijdregistratiesDto;
            }
            catch (DatabaseException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateTijdregistratieTime(int tijdregistratieId, TimeSpan nieuweTijd) //mss met object zelf werken, vermijden van ophalen via id
        {
            try
            {
                Tijdregistratie tijdregistratie = await _tijdRepository.GetTijdregistratieById(tijdregistratieId);

                DateTime nieuweStopTijd = new DateTime(
                         tijdregistratie.StopTijd.Value.Year,
                         tijdregistratie.StopTijd.Value.Month,
                         tijdregistratie.StopTijd.Value.Day,
                         nieuweTijd.Hours,
                         nieuweTijd.Minutes,
                         nieuweTijd.Seconds);

                // Update de tijd met behoud van de datum
                await _tijdRepository.UpdateStopTijdById(tijdregistratieId, nieuweStopTijd);
            }
            catch (DatabaseException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<TijdregistratieDto> GetTijdregistratieById(int id)
        {
            try
            {
                Tijdregistratie tijdregistratie = await _tijdRepository.GetTijdregistratieById(id);
                TijdregistratieDto dto = _mapper.Map<TijdregistratieDto>(tijdregistratie);

                return dto;
            }
            catch (DatabaseException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task UpdateTijdregistratie(int id, TijdregistratieDto tijdregistratieDto)
        {

            try
            {
                Tijdregistratie tijdregistratie = _mapper.Map<Tijdregistratie>(tijdregistratieDto);
                await _tijdRepository.UpdateTijdregistratieById(id, tijdregistratie);
            }
            catch (DatabaseException ex)
            {
                throw;
            }
            catch (Exception ex)
            {

            }
        }
    }
}

