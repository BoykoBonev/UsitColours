using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using UsitColours.Data.Contracts;

namespace UsitColours.Data.Repositories
{

    public class EfGenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IDbContext context;

        public EfGenericRepository(IDbContext context)
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