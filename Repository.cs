using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morphological_generator.EntityFramework.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly MorphologicalContext _dbContext;

        private readonly DbSet<TEntity> DbEntity;
        public Repository(MorphologicalContext dbContext)
        {
            _dbContext = dbContext;
            DbEntity = _dbContext.Set<TEntity>();
                
        }
        public void Add(TEntity entity)
        {
            DbEntity.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            DbEntity.Remove(entity);
        }

        public TEntity Get(Func<TEntity, bool> predicate)
        {
            return DbEntity.FirstOrDefault(predicate);
        }

        public IEnumerable<TEntity> GetAll(Func<TEntity, bool> predicate = null)
        {
            if (predicate != null)
                return DbEntity.Where(predicate);
            return DbEntity;
        }

        public void Update(TEntity entity)
        {
            DbEntity.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
