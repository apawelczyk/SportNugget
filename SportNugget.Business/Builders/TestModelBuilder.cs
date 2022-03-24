using AutoMapper;
using SportNugget.Business.Builders.Interfaces;
using SportNugget.BusinessModels.Test;
using SportNugget.Data.Models;

namespace SportNugget.Business.Builders
{
    public class TestModelBuilder : ITestModelBuilder
    {
        private readonly IMapper _mapper;

        public TestModelBuilder(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TestModel Build(Test dataModel)
        {
            if(dataModel == null)
            {
                return null;
            }

            return _mapper.Map<TestModel>(dataModel);
        }
    }
}
