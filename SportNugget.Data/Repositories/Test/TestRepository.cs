using Microsoft.EntityFrameworkCore;
using SportNugget.Data.Contexts;
using SportNugget.Data.Repositories.Base;
using SportNugget.Data.Repositories.Interfaces;
using SportNugget.Logging.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SportNugget.Data.Repositories.Test
{
    public class TestRepository : RepositoryBase<SportNugget.Data.Models.Test>, ITestRepository
    {
        private SportnuggetContext _sportnuggetContext;
        private readonly ILogger _logger;

        public TestRepository(SportnuggetContext sportnuggetContext, ILogger logger) : base(sportnuggetContext, logger)
        {
            _sportnuggetContext = sportnuggetContext;
            _logger = logger;
        }

        public async Task<Models.Test> GetById(int id)
        {
            try
            {
                return await _sportnuggetContext.Tests.Where(x => x.PkId == id).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error retrieving TestModel by id in Repository.");
                return null;
            }
        }
    }
}
