using IntegraHub.Domain.Entities;
using IntegraHub.Domain.Interfaces;
using IntegraHub.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace IntegraHub.Infra.Data.Repository
{
    public abstract class BaseRepository<TEntity, TKey>(PostgresContext pgContext) : IBaseRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected readonly PostgresContext _postgresContext = pgContext;

        public IQueryable<TEntity> Query() => _postgresContext.Set<TEntity>();  
        public async Task<TKey> Insert(TEntity obj)
        {
            _postgresContext.Set<TEntity>().Add(obj).State = EntityState.Added;
            await _postgresContext.SaveChangesAsync();
            return obj.Id;
        }

        public async Task Update(TEntity obj)
        {
            _postgresContext.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _postgresContext.SaveChangesAsync();
        }

        public async Task Delete(TKey id)
        {
            TEntity? obj = await Select(id) ?? throw new Exception($"{typeof(TEntity).Name} with key {id} not found!");

            _postgresContext.Set<TEntity>().Remove(obj).State = EntityState.Deleted;
            await _postgresContext.SaveChangesAsync();
        }

        public async Task<IList<TEntity>> Select() =>
            await _postgresContext.Set<TEntity>().ToListAsync();

        public async Task<TEntity?> Select(TKey id) =>
            await _postgresContext.Set<TEntity>().FindAsync(id);

    }
}
