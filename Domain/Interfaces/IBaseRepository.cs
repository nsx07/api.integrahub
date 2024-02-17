using IntegraHub.Domain.Entities;

namespace IntegraHub.Domain.Interfaces
{
    public interface IBaseRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        IQueryable<TEntity> Query();

        Task<TKey> Insert(TEntity obj);

        Task Update(TEntity obj);

        Task Delete(TKey id);

        Task<IList<TEntity>> Select();

        Task<TEntity?> Select(TKey id);
    }
}
