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

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> temp = dbSet;
            if (filter != null)
            {
                temp = temp.Where(filter);
            }
            return await temp.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<T> temp = dbSet;
            if (!tracked == true)
            {
                temp = temp.AsNoTracking();
            }
            if (filter != null)
            {
                temp = temp.Where(filter);
            }
            return await temp.FirstOrDefaultAsync() ?? default!;
        }

        public async Task RemoveAsync(T entity)
        {
            dbSet.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _pandaContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            dbSet.Update(entity);
            await SaveAsync();
        }
    }
}