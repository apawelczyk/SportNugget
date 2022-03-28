using AutoMapper;
using SportNugget.BusinessModels.Test;
using SportNugget.ViewModels.Demos;

namespace SportNugget.Shared.MappingConfigurations
{
    public class ViewModelMappingProfile : Profile
    {
        public ViewModelMappingProfile()
        {
            MapTestModels();
        }

        public void MapTestModels()
        {
            CreateMap<TestViewModel, TestModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TestText, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();
        }
    }
}
