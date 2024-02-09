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

                //voeg dezelfde werf toe aan de WerfCopyTabel --> hier hangen tijdregistraties van af, anders problemen indien een werf verwijderd wordt.



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
           List<Werf> werven =  await _werfRepository.GetAllWervenAsync();

            List<WerfDto> wervenDto = _mapper.Map<List<WerfDto>>(werven);

            return wervenDto;
        }


    }



}



