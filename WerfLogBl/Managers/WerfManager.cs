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
    public class WerfManager: IWerfManager
    {


        private readonly IWerfRepository _werfRepository;
        private readonly IMapper _mapper;
        public WerfManager(IWerfRepository werfRepository, IMapper mapper) 
        {
            _mapper = mapper;
            _werfRepository = werfRepository;
        }

        //public WerfDto AddWerf(WerfDto werfDto)
        //{
        //    // Converteer de DTO naar het DAL-model Werf
        //    var werf = new Werf
        //    {
        //        Naam = werfDto.Naam
        //        // Vul andere eigenschappen in als dat nodig is
        //    };

        //    // Voeg de Werf toe aan de database en krijg het resultaat terug (inclusief de ID)
        //    var nieuweWerf = _werfRepository.InsertWithReturn(werf);

        //    // Creëer een nieuwe WerfDto op basis van de informatie in nieuweWerf
        //    var nieuweWerfDto = new WerfDto
        //    {
        //        Id = nieuweWerf.Id,
        //        Naam = nieuweWerf.Naam
        //        // Kopieer eventueel andere eigenschappen van nieuweWerf naar nieuweWerfDto
        //    };

        //    return nieuweWerfDto; // Retourneer het WerfDto object


        //}

        public WerfDto AddWerf(WerfDto werfDto)
        {
            // Map WerfDto naar Werf
            Werf werf = _mapper.Map<Werf>(werfDto);

            // Voeg de Werf toe aan de database en krijg het resultaat terug
            Werf nieuweWerf = _werfRepository.InsertWithReturn(werf);

            // Map het resultaat (Werf) terug naar WerfDto
            WerfDto nieuweWerfDto = _mapper.Map<WerfDto>(nieuweWerf);

            return nieuweWerfDto; // Retourneer het WerfDto object
        }
    }
}



