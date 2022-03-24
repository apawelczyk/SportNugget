using SportNugget.Data.Models;
using System.Threading.Tasks;

namespace SportNugget.Data.Repositories.Interfaces
{
    public interface ITestRepository : IRepositoryBase<SportNugget.Data.Models.Test>
    {
        Task<SportNugget.Data.Models.Test> GetById(int id);
    }
}
