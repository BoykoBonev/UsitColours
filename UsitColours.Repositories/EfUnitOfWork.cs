using System;
using UsitColours.Data.Contracts;
using UsitColours.Repositories.Contracts;

namespace UsitColours.Repositories
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly IDbContext context;

        public EfUnitOfWork(IDbContext context)
        {
            if (context == null)
            {
                throw new NullReferenceException("DbContext");
            }

            this.context = context;
        }

        public void Commit()
        {
            this.context.SaveChanges();
        }

        public void Dispose()
        {
        }
    }
}
