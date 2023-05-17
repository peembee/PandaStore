using Microsoft.EntityFrameworkCore;
using PandaStore.Repository.Irepository;
using System.Linq.Expressions;
using System.Linq;
using PandaStore.Data;

namespace PandaStore.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly PandaStoreContext _pandaContext;
        internal DbSet<T> dbSet;

        public Repository(PandaStoreContext pandaContext)
        {
            _pandaContext = pandaContext;
            dbSet = _pandaContext.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public async Task SaveAsync()
        {
            await _pandaContext.SaveChangesAsync();
        }
    }
}