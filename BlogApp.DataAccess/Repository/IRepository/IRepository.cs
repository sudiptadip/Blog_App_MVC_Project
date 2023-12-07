using System.Linq.Expressions;

namespace BlogApp.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter, string IncludePropertyes = null);
        IEnumerable<T> GetAll(string IncludePropertyes = null);
        T Get(Expression<Func<T, bool>> filter);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> items);
        void Add(T entity);
    }
}
