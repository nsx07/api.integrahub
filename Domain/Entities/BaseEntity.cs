﻿namespace IntegraHub.Domain.Entities
{
    public abstract class BaseEntity<TKey>
    {
        public virtual TKey Id { get; set; }
    }
}
