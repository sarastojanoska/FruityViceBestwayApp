using AutoMapper;
using FruityViceBestwayApp.Entities;
using FruityViceBestwayApp.Models.ViewModels;

namespace FruityViceBestwayApp.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Fruit, FruitViewModel>()
                .ForMember(dest => dest.Nutritions, opt => opt.MapFrom(src => src.Nutrition));

            CreateMap<Nutrition, NutritionsViewModel>();

            CreateMap<FruitViewModel, Fruit>()
                .ForMember(dest => dest.Nutrition, opt => opt.MapFrom(src => src.Nutritions))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<NutritionsViewModel, Nutrition>();
        }
    }
}
