using System.Linq;

namespace UsitColours.Data.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> All { get;}

        T GetById(object id);

        void Add(T entity);

        void Delete(T entity);

        void Update(T entity);
    }
}
