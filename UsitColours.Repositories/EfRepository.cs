using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using UsitColours.Data.Contracts;
using UsitColours.Repositories.Contracts;

namespace UsitColours.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : class
    {
        private readonly IDbContext context;

        public EfRepository(IDbContext context)
        {
            if (context == null)
            {
                throw new NullReferenceException("DbContext");
            }

            this.context = context;

            this.DbSet = this.context.Set<T>();
        }

        protected DbSet<T> DbSet { get; set; }

        public IQueryable<T> All
        {
            get { return this.DbSet; }
        }

        public T GetById(object id)
        {
            return this.DbSet.Find(id);
        }

        public IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includeExpressions)
        {
            return this.GetAll(null, includeExpressions);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filterExpression, params Expression<Func<T, object>>[] includeExpressions)
        {
            return this.GetAll<object>(filterExpression, null, includeExpressions);
        }

        public IEnumerable<T> GetAll<T1>(Expression<Func<T, bool>> filterExpression, Expression<Func<T, T1>> sortExpression, params Expression<Func<T, object>>[] includeExpressions)
        {
            return this.GetAll<T1, T>(filterExpression, sortExpression, null, includeExpressions);
        }

        public IEnumerable<T2> GetAll<T1, T2>(Expression<Func<T, bool>> filterExpression, Expression<Func<T, T1>> sortExpression, Expression<Func<T, T2>> selectExpression, params Expression<Func<T, object>>[] includeExpressions)
        {
            IQueryable<T> result = this.DbSet;

            if (filterExpression != null)
            {
                result = result.Where(filterExpression);
            }

            if (sortExpression != null)
            {
                result = result.OrderBy(sortExpression);
            }

            if (selectExpression != null)
            {

                foreach (var i in includeExpressions)
                {
                    result = result.Include(i);
                }

                return result.Select(selectExpression).ToList();
            }
            else
            {
                foreach (var i in includeExpressions)
                {
                    result = result.Include(i);
                }

                return result.OfType<T2>().ToList();
            }
        }

        public void Add(T entity)
        {
            var entry = AttachIfDetached(entity);
            entry.State = EntityState.Added;
        }

        public void Update(T entity)
        {
            var entry = AttachIfDetached(entity);
            entry.State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            var entry = AttachIfDetached(entity);
            entry.State = EntityState.Deleted;
        }

        private DbEntityEntry AttachIfDetached(T entity)
        {
            DbEntityEntry entry;
            try
            {
                entry = this.context.Entry(entity);

            }
            catch (Exception e)
            {
                throw;
            }

            if (entry.State == EntityState.Detached)
            {
                try
                {
                    this.DbSet.Attach(entity);

                }
                catch (Exception e)
                {

                }
            }

            return entry;
        }
    }
}
