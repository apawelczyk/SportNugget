using SportNugget.BusinessModels.Test;
using SportNugget.Common.API;
using System.Threading.Tasks;

namespace SportNugget.Business.Services.Interfaces
{
    public interface ITestService
    {
        Task<ResponseWrapper<TestModel>> GetTest(int id);
        Task<ResponseWrapper<List<TestModel>>> GetAllTests();
    }
}
