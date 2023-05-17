using System.Linq.Expressions;

namespace PandaStore.Repository.Irepository
{
    public interface IRepository<T> where T : class
    {         
        public interface IRepository<T> where T : class
        {
            Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
            Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true);
            Task CreateAsync(T entity);
            Task RemoveAsync(T entity);
            Task UpdateAsync(T entity);
            Task SaveAsync();
        }
    }
}

