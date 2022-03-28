using SportNugget.BusinessModels.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportNugget.Shared.Services.Interfaces
{
    public interface ITestService
    {
        Task<List<TestModel>> GetTests();
    }
}
