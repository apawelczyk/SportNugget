using AutoMapper;
using SportNugget.Business.Builders.Interfaces;
using SportNugget.Business.DataAccess.Interfaces;
using SportNugget.Business.Services.Interfaces;
using SportNugget.BusinessModels.Test;
using SportNugget.Common.API;
using SportNugget.Data.Models;
using SportNugget.Data.Repositories.Interfaces;
using SportNugget.Logging.Interfaces;
using System;
using System.Threading.Tasks;

namespace SportNugget.Business.Services
{
    public class TestService : ITestService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILogger _logger;
        private readonly ITestModelBuilder _testModelBuilder;
        private readonly IMapper _mapper;

        public TestService(IRepositoryWrapper repositoryWrapper, ILogger logger, ITestModelBuilder testModelBuilder, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
            _testModelBuilder = testModelBuilder;
            _mapper = mapper;
        }

        public async Task<ResponseWrapper<TestModel>> GetTest(int id)
        {
            var responseWrapper = new ResponseWrapper<TestModel>()
            {
                Data = null,
                IsSuccessful = false,
                Exception = null
            };

            try
            {
                var test = await _repositoryWrapper.Test.GetById(id);
                responseWrapper.Data = _testModelBuilder.Build(test);
                responseWrapper.IsSuccessful = true;
                return responseWrapper;
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Error getting Test by id in service.");
                responseWrapper.Exception = e.Message;
                return responseWrapper;
            }
        }

        public async Task<ResponseWrapper<List<TestModel>>> GetAllTests()
        {
            var responseWrapper = new ResponseWrapper<List<TestModel>>()
            {
                Data = new List<TestModel>(),
                IsSuccessful = false,
                Exception = null
            };

            try
            {
                var tests = await _repositoryWrapper.Test.GetAll();
                // Abstract an extra layer
                responseWrapper.Data = _testModelBuilder.BuildMany(tests?.ToList());
                // Or just map
                responseWrapper.Data = _mapper.Map<List<Test>, List<TestModel>>(tests?.ToList());
                responseWrapper.IsSuccessful = true;
                return responseWrapper;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting Test by id in service.");
                responseWrapper.Exception = e.Message;
                return responseWrapper;
            }
        }
    }
}
