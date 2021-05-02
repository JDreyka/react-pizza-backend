using AutoMapper;
using react.pizza.backend.Models;

namespace react.pizza.backend.Configs
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CatalogItemDto, CatalogItem>();
        }
    }
}