using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
namespace UsitColours.Repositories.Contracts
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> All { get; }

        void Add(T entity);

        void Delete(T entity);

        IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includeExpressions);

        IEnumerable<T> GetAll(Expression<Func<T, bool>> filterExpression, params Expression<Func<T, object>>[] includeExpressions);

        IEnumerable<T> GetAll<T1>(Expression<Func<T, bool>> filterExpression, Expression<Func<T, T1>> sortExpression, params Expression<Func<T, object>>[] includeExpressions);

        IEnumerable<T2> GetAll<T1, T2>(Expression<Func<T, bool>> filterExpression, Expression<Func<T, T1>> sortExpression, Expression<Func<T, T2>> selectExpression, params Expression<Func<T, object>>[] includeExpressions);

        T GetById(object id);

        void Update(T entity);
    }
}
