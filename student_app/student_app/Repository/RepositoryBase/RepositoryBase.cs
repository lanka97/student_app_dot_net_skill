using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using student_app.Models;

namespace student_app.Repository.RepositoryBase
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected APPDBContext ApplicationContext;
        public RepositoryBase(APPDBContext _ApplicationContext)
        {
            ApplicationContext = _ApplicationContext;
        }

        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges ?
              ApplicationContext.Set<T>()
                .AsNoTracking() :
              ApplicationContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            !trackChanges ?
              ApplicationContext.Set<T>()
                .Where(expression)
                .AsNoTracking() :
              ApplicationContext.Set<T>()
                .Where(expression);

        public void Create(T entity) => ApplicationContext.Set<T>().Add(entity);

        public void Update(T entity) => ApplicationContext.Set<T>().Update(entity);

        public void Delete(T entity) => ApplicationContext.Set<T>().Remove(entity);
    }
}
