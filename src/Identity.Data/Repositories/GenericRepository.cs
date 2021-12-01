using Identity.Business.Entities;
using Identity.Business.Interfaces.Repositories;
using Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace Valorem.Core.Infrastructure
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity>, IDisposable where TEntity : class, IEntidadeBase
    {
        private readonly ApplicationDbContext _context;

        protected GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;  
                
        public IQueryable<TEntity> Get() => _context.Set<TEntity>().AsQueryable();

        public IQueryable<TEntity> GetAsNoTracking() => Get().AsNoTrackingWithIdentityResolution();

        public void Adicionar(TEntity entity) => _context.Add(entity);

        public void Update(TEntity entity) => _context.Entry(entity).State = EntityState.Modified;

        public void Remove(TEntity entity) => _context.Remove(entity);

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}