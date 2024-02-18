using AutoMapper;
using WerfLogBl.DTOS;
using WerfLogBl.Interfaces;
using WerfLogDal.Exceptions;
using WerfLogDal.Interfaces;
using WerfLogDal.Models;

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

        public async Task<List<NotitieDto>> GetAllNotities(int werfId)
        {
            try
            {
                List<Notitie> notities = await _notitieRepository.GetAllNotitiesByWerfIdAsync(werfId);

                List<NotitieDto> notitieDtos = _mapper.Map<List<NotitieDto>>(notities);

                return notitieDtos;
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

        public async Task<NotitieDto> AddNotitieAsync(NotitieDto notitieDto)
        {
            try
            {
                // Map NotitieDto naar Notitie met behulp van AutoMapper
                Notitie notitie = _mapper.Map<Notitie>(notitieDto);

                // Voeg de Notitie toe aan de database en wacht op het resultaat
                // Dit veronderstelt dat InsertWithReturnAsync een asynchrone methode is die een Notitie object teruggeeft
                Notitie nieuweNotitie = await _notitieRepository.InsertWithReturnAsync(notitie);

                // Map het resultaat (Notitie) terug naar NotitieDto met behulp van AutoMapper
                NotitieDto nieuweNotitieDto = _mapper.Map<NotitieDto>(nieuweNotitie);

                // Retourneer het NotitieDto object
                return nieuweNotitieDto;
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

        public async Task DeleteNotitieAsync(NotitieDto notitieDto)
        {
            try
            {
                // Map NotitieDto naar Notitie met behulp van AutoMapper
                Notitie notitie = _mapper.Map<Notitie>(notitieDto);

                // Voeg de Notitie toe aan de database en wacht op het resultaat
                // Dit veronderstelt dat InsertWithReturnAsync een asynchrone methode is die een Notitie object teruggeeft
                int row = await _notitieRepository.Delete(notitie);

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
