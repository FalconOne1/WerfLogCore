using AutoMapper;
using WerfLogBl.DTOS;
using WerfLogBl.Interfaces;
using WerfLogDal.Exceptions;
using WerfLogDal.Interfaces;
using WerfLogDal.Models;

namespace WerfLogBl.Managers
{
    public class WerfManager : IWerfManager
    {

        private readonly IWerfRepository _werfRepository;
        private readonly IMapper _mapper;
        public WerfManager(IWerfRepository werfRepository, IMapper mapper)
        {
            _mapper = mapper;
            _werfRepository = werfRepository;
        }

        public async Task<WerfDto> AddWerfAsync(WerfDto werfDto)
        {
            try
            {
                // Map WerfDto naar Werf.
                Werf werf = _mapper.Map<Werf>(werfDto);

                // Voeg de Werf toe aan de database en wacht op het resultaat.
                Werf nieuweWerf = await _werfRepository.InsertWithReturnAsync(werf);

                // Map het resultaat (Werf) terug naar WerfDto.
                WerfDto nieuweWerfDto = _mapper.Map<WerfDto>(nieuweWerf);

                return nieuweWerfDto; // Retourneer het WerfDto object.
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

        public async Task<List<WerfDto>> HaalAlleWervenOp()
        {
            try
            {
                List<Werf> werven = await _werfRepository.GetAllWervenAsync();
                List<WerfDto> wervenDto = _mapper.Map<List<WerfDto>>(werven);
                return wervenDto;
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

        public async Task DeleteWerfAsync(WerfDto werfDto)
        {
            try
            {
                // Map NotitieDto naar Notitie met behulp van AutoMapper
                Werf werf = _mapper.Map<Werf>(werfDto);

                // Voeg de Notitie toe aan de database en wacht op het resultaat
                // Dit veronderstelt dat InsertWithReturnAsync een asynchrone methode is die een Notitie object teruggeeft
                int row = await _werfRepository.Delete(werf);

                // Map het resultaat (Notitie) terug naar NotitieDto met behulp van AutoMapper

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








    }



}



