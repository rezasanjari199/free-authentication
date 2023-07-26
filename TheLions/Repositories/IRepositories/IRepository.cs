using System.Linq.Expressions;

namespace TheLions.Repositories.IRepositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        //Get
        TEntity Get(int id);
        TEntity Get(long id);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeExpressions);

        //Add
        Task AddAsync(TEntity entity);
        void AddRangeAsync(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        //Remove
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
