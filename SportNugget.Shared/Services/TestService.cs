using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using SportNugget.BusinessModels.Test;
using SportNugget.Common.API;
using SportNugget.Common.API.Interfaces;
using SportNugget.Logging.Interfaces;
using SportNugget.Shared.Services.Interfaces;

namespace SportNugget.Shared.Services
{
    public class TestService : ITestService
    {
        private readonly IDataAccessManager _dataAccessManager;
        private readonly ILogger _logger;

        public TestService(IDataAccessManager dataAccessManager, ILogger logger)
        {
            _dataAccessManager = dataAccessManager;
            _logger = logger;
        }

        public async Task<List<TestModel>> GetTests()
        {
            var tests = await _dataAccessManager.Get<ResponseWrapper<List<TestModel>>>("api/Test");
            return tests?.Data;
        }
    }
}
