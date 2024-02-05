using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WerfLogBl.DTOS;
using WerfLogDal.Models;


namespace WerfLogBl.AutoMapperProfiles
{
    public class DtoMappers : Profile
    {
        public DtoMappers()
        {
            //zonder afwijkingen, dus copy paste
            CreateMap<Werf, WerfDto>().ReverseMap();

            CreateMap<Notitie, NotitieDto>().ReverseMap();

            CreateMap<Tijdregistratie, TijdregistratieDto>().ReverseMap();

            CreateMap<ProjectOverleg, ProjectOverlegDto>().ReverseMap();
        }
    }
}
