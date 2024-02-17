using FluentValidation;
using IntegraHub.Domain.Entities;

namespace IntegraHub.Domain.Interfaces
{
    public interface IBaseService<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<TEntity> Add<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>;

        Task Delete(TKey id);

        Task<IList<TEntity>> Get();

        Task<TEntity?> GetById(TKey id);

        Task<TEntity> Update<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>;
    }
}
