﻿using SportNugget.Business.Builders.Interfaces;
using SportNugget.Business.DataAccess.Interfaces;
using SportNugget.Business.Services.Interfaces;
using SportNugget.BusinessModels.Test;
using SportNugget.Common.API;
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

        public TestService(IRepositoryWrapper repositoryWrapper, ILogger logger, ITestModelBuilder testModelBuilder)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
            _testModelBuilder = testModelBuilder;
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
                responseWrapper.Exception = e;
                return responseWrapper;
            }
        }
    }
}