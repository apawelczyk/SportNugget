using Microsoft.EntityFrameworkCore;
using SportNugget.Data.Contexts;
using SportNugget.Data.Repositories.Interfaces;
using SportNugget.Logging.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SportNugget.Data.Repositories.Base
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private SportnuggetContext _sportnuggetContext;
        private readonly ILogger _logger;

        public RepositoryBase(SportnuggetContext sportnuggetContext, ILogger logger)
        {
            _sportnuggetContext = sportnuggetContext;
            _logger = logger;
        }

        public async Task Create(T entity)
        {
            try
            {
                await _sportnuggetContext.Set<T>().AddAsync(entity);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error creating entity.");
                throw e;
            }
        }

        public async Task Delete(T entity)
        {
            try
            {
                _sportnuggetContext.Set<T>().Remove(entity);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error deleting entity.");
                throw e;
            }
        }

        public async Task<IQueryable<T>> GetAll()
        {
            try
            {
                return _sportnuggetContext.Set<T>();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error retrieving entity.");
                throw e;
            }
        }

        public async Task<IQueryable<T>> GetAll(Expression<Func<T, bool>> expression)
        {
            try
            {
                return _sportnuggetContext.Set<T>().Where(expression);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error retrieving entity.");
                throw e;
            }
        }

        public async Task Update(T entity)
        {
            try
            {
                _sportnuggetContext.Set<T>().Update(entity);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error updating entity.");
                throw e;
            }
        }
    }
}
