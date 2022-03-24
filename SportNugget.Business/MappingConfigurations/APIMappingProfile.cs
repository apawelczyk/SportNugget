using AutoMapper;
using SportNugget.BusinessModels.Test;
using SportNugget.Data.Models;

namespace SportNugget.Business.MappingConfigurations
{
    public class APIMappingProfile : Profile
    {
        public APIMappingProfile()
        {
            MapTestModels();
        }

        public void MapTestModels()
        {
            CreateMap<TestModel, Test>()
                .ForMember(dest => dest.PkId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TestName, opt => opt.MapFrom(src => src.TestText))
                .ReverseMap();
        }
    }
}
