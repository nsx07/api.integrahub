﻿using FluentValidation;
using IntegraHub.Domain.Entities;
using IntegraHub.Domain.Interfaces;

namespace IntegraHub.Service.Services
{
    public abstract class BaseService<TEntity, TKey>(IBaseRepository<TEntity, TKey> baseRepository) : IBaseService<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly IBaseRepository<TEntity, TKey> _baseRepository = baseRepository;

        public async Task<TEntity> Add<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>
        {
            Validate(obj, Activator.CreateInstance<TValidator>());
            await _baseRepository.Insert(obj);
            return obj;
        }

        public async Task Delete(TKey id) => await _baseRepository.Delete(id);

        public async Task<IList<TEntity>> Get() => await _baseRepository.Select();

        public async Task<TEntity?> GetById(TKey id) => await _baseRepository.Select(id);

        public async Task<TEntity> Update<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>
        {
            Validate(obj, Activator.CreateInstance<TValidator>());
            await _baseRepository.Update(obj);
            return obj;
        }

        private static void Validate(TEntity obj, AbstractValidator<TEntity> validator)
        {
            if (obj == null)
                throw new Exception("Registros não detectados!");

            validator.ValidateAndThrow(obj);
        }
    }
}