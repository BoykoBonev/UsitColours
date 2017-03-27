using System.Data.Entity;
using UsitColours.Data.Contracts;
using UsitColours.Data.Repositories;

namespace UsitColours.Tests.Data.Mocks
{
    public class EfRepositoryMock<T> : EfGenericRepository<T> where T : class
    {
        public EfRepositoryMock(IDbContext context) : base(context)
        {
        }

        public DbSet<T> MyDbSet
        {
            get
            {
                return base.DbSet;
            }
        }
    }
}
