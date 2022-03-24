using SportNugget.Business.DataAccess.Interfaces;
using SportNugget.Data.Contexts;
using SportNugget.Data.Repositories.Interfaces;
using SportNugget.Data.Repositories.Test;
using SportNugget.Logging.Interfaces;
using System;
using System.Threading.Tasks;

namespace SportNugget.Business.DataAccess
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private SportnuggetContext _sportnuggetContext;
        private ILogger _logger;
        private ITestRepository _testRepository;

        public RepositoryWrapper(SportnuggetContext sportnuggetContext, ILogger logger)
        {
            _sportnuggetContext = sportnuggetContext;
            _logger = logger;
        }

        public ITestRepository Test {
            get
            { 
                if(_testRepository == null)
                {
                    _testRepository = new TestRepository(_sportnuggetContext, _logger);
                }
                return _testRepository;
            }
            set 
            {
                value = _testRepository;
            }
        }

        public async Task Save()
        {
            await _sportnuggetContext.SaveChangesAsync();
        }
    }
}
