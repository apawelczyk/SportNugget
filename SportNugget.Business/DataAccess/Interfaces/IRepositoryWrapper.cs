using SportNugget.Data.Repositories.Interfaces;
using System.Threading.Tasks;

namespace SportNugget.Business.DataAccess.Interfaces
{
    public interface IRepositoryWrapper
    {
        ITestRepository Test { get; set; }
        Task Save();
    }
}
