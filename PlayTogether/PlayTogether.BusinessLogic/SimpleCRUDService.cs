using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PlayTogether.DataAccess;
using PlayTogether.Domain;

namespace PlayTogether.BusinessLogic
{
    public class SimpleCRUDService : ISimpleCRUDService
    {
        private readonly IGenericRepository _repository;

        public SimpleCRUDService(IGenericRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<TEntity>> GetAll<TEntity>()
            where TEntity : class, ISimpleEntity
        {
            return await _repository.GetAll<TEntity>();
        }

        public async Task<ICollection<TEntity>> Where<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class, ISimpleEntity
        {
            return await _repository.Where<TEntity>(predicate);
        }

        public async Task<TEntity> GetById<TEntity>(Guid id)
            where TEntity : class, ISimpleEntity
        {
            return await _repository.Find<TEntity>(l => l.Id == id);
        }

        public async Task<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, ISimpleEntity
        {
            return (await Where(predicate)).FirstOrDefault();
        }

        public async Task<TNewEntity> Create<TNewEntity>(TNewEntity newEntity)
            where TNewEntity : class, ISimpleEntity
        {
            _repository.Add(newEntity);
            await _repository.SaveChanges();
            return newEntity;
        }

        public async Task<TOldEntity> Update<TNewEntity, TOldEntity>(Guid id, TNewEntity entity, Action<TOldEntity, TNewEntity> updateFunc)
            where TOldEntity : class, ISimpleEntity
        {
            var dbEntity = await GetById<TOldEntity>(id);
            if (dbEntity != null)
            {
                updateFunc(dbEntity, entity);
                await _repository.SaveChanges();
            }

            return dbEntity;
        }

        public async Task RemoveById<TEntity>(Guid id)
            where TEntity : class, ISimpleEntity
        {
            var entity = await GetById<TEntity>(id);
            _repository.Remove(entity);
            await _repository.SaveChanges();
        }
    }
}