using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SportNugget.Data.Repositories.Interfaces
{
    public interface IRepositoryBase<T>
    {
        Task<IQueryable<T>> GetAll();
        Task<IQueryable<T>> GetAll(Expression<Func<T, bool>> expression);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
