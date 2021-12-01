using Identity.Business.Entities;

namespace Identity.Business.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntidadeBase
    {
        IUnitOfWork UnitOfWork { get; }
        void Adicionar(TEntity entity);
        IQueryable<TEntity> Get();
        IQueryable<TEntity> GetAsNoTracking();
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}