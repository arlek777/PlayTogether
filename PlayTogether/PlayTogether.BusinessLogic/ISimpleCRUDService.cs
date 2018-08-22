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

        Task<TEntity> CreateOrUpdate<TEntity>(TEntity entity, Action<TEntity, TEntity> updateFunc)
            where TEntity : class, ISimpleEntity;

        Task RemoveById<TEntity>(Guid id)
            where TEntity : class, ISimpleEntity;
    }
}
