using SportNugget.BusinessModels.Test;
using SportNugget.Common.API;
using SportNugget.Common.API.Interfaces;
using SportNugget.Shared.Services.Interfaces;

namespace SportNugget.Shared.Services
{
    public class TestService : ITestService
    {
        private readonly IDataAccessManager _dataAccessManager;

        public TestService(IDataAccessManager dataAccessManager)
        {
            _dataAccessManager = dataAccessManager;
        }

        public async Task<List<TestModel>> GetTests()
        {
            var tests = await _dataAccessManager.Get<ResponseWrapper<List<TestModel>>>("api/Test");
            return tests?.Data;
        }
    }
}
