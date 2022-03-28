using AutoMapper;
using SportNugget.BusinessModels.Test;
using SportNugget.Shared.ViewModelBuilders.Interfaces;
using SportNugget.ViewModels.Demos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportNugget.Shared.ViewModelBuilders
{
    public class TestViewModelBuilder : ITestViewModelBuilder
    {
        private readonly IMapper _mapper;

        public TestViewModelBuilder(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TestViewModel Build(TestModel businessModel)
        {
            return _mapper.Map<TestViewModel>(businessModel);
        }

        public List<TestViewModel> BuildMany(List<TestModel> businessModels)
        {
            return _mapper.Map< List<TestModel>, List <TestViewModel>>(businessModels);
        }
    }
}
