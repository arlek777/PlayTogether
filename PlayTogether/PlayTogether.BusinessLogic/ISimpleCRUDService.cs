using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PlayTogether.Domain;

namespace PlayTogether.BusinessLogic
{
    public interface ISimpleCRUDService
    {
        Task<ICollection<TEntity>> GetAll<TEntity>()
            where TEntity : class, ISimpleEntity;

        Task<ICollection<TEntity>> Where<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class, ISimpleEntity;

        Task<TEntity> GetById<TEntity>(Guid id)
            where TEntity : class, ISimpleEntity;

        Task<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class, ISimpleEntity;

        Task<TNewEntity> Create<TNewEntity>(TNewEntity newEntity)
            where TNewEntity : class, ISimpleEntity;

        Task<TToEntity> Update<TFromEntity, TToEntity>(Guid id, TFromEntity entity,
            Action<TToEntity, TFromEntity> updateFunc)
            where TToEntity : class, ISimpleEntity;

        Task RemoveById<TEntity>(Guid id)
            where TEntity : class, ISimpleEntity;
    }
}
