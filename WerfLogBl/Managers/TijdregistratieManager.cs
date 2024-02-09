using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WerfLogBl.DTOS;
using WerfLogBl.Interfaces;
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
                    WerfNaamRegistratie = werfNaam
                    
    };

                 Tijdregistratie nieuweTijdregistratie = await _tijdRepository.InsertWithReturnAsync(tijdregistratie);

                return (int)nieuweTijdregistratie.Id;
            }
            else
            {
                throw new Exception("Fout met werfID");
            }
        }

        public async Task VoegStoptijdWerfToe(int actieveTijdId)
        {
             await _tijdRepository.UpdateStopTijdById(actieveTijdId, DateTime.Now);


        }

        public async Task<TijdregistratieDto> GetActieveTijdregistratieId()
        {
          Tijdregistratie sessie = await _tijdRepository.GetEmptyStopDateTimeAsync();

            
           return _mapper.Map<TijdregistratieDto>(sessie);
          
        }



        public async Task UpdateTijdregistratieTime(int tijdregistratieId, TimeSpan nieuweTijd) //mss met object zelf werken, vermijden van ophalen via id
        {
          Tijdregistratie tijdregistratie =await _tijdRepository.GetTijdregistratieById(tijdregistratieId);

       
           DateTime nieuweStopTijd = new DateTime(
                    tijdregistratie.StopTijd.Value.Year,
                    tijdregistratie.StopTijd.Value.Month,
                    tijdregistratie.StopTijd.Value.Day,
                    nieuweTijd.Hours,
                    nieuweTijd.Minutes,
                    nieuweTijd.Seconds);

            // Update de tijd met behoud van de datum


            await  _tijdRepository.UpdateStopTijdById(tijdregistratieId, nieuweStopTijd);
            }
            //}

        }
}
