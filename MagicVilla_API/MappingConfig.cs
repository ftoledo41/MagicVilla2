using AutoMapper;
using MagicVilla_API.Dto;
using MagicVilla_API.Models;

namespace MagicVilla_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaDto>(); //Fuente, Destino
            CreateMap<VillaDto, Villa>(); //Destino, fuente

            CreateMap<Villa, VillaCreateDto>().ReverseMap(); //Fuente, Destino
            CreateMap<Villa, VillaUpdateDto>().ReverseMap(); //Fuente, Destino
        }    
    }
}
