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
using WerfLogDal.Repositories;

namespace WerfLogBl.Managers
{
   public class NotitieManager : INotitieManager
    {

        private readonly INotitieRepository _notitieRepository;
        private readonly IMapper _mapper;
        public NotitieManager(INotitieRepository notitieRepository, IMapper mapper)
        {
            _mapper = mapper;   
            _notitieRepository = notitieRepository;
        }



        //public void AddNotitie(NotitieDto notitieDto)
        //{
        //    // Converteer de DTO naar het DAL-model Notitie
        //    var notitie = new Notitie
        //    {
        //        Datum = notitieDto.Datum,
        //        Tekst = notitieDto.Tekst,
        //        WerfId = notitieDto.WerfId
                
        //        // Vul andere eigenschappen in als dat nodig is
        //    };

        //    // Voeg de Notitie toe aan de database
        //     _notitieRepository.InsertWithNoReturn(notitie);
        //}

        public NotitieDto AddNotitie(NotitieDto notitieDto)
        {
            // Map NotitieDto naar Notitie
            Notitie notitie = _mapper.Map<Notitie>(notitieDto);

            // Voeg de Notitie toe aan de database
           Notitie nieuweNotitie =  _notitieRepository.InsertWithReturn(notitie);

            NotitieDto nieuweNotitieDto = _mapper.Map<NotitieDto>(nieuweNotitie);

            return nieuweNotitieDto;


        }

    }
}
