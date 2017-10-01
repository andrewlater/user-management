using Microsoft.EntityFrameworkCore;
using System;

namespace UserManagement.Core.Data
{
    public interface IDataSession : IDisposable
    {
        DbSet<T> Set<T>() where T : class;
        void SaveChanges();
    }

    public class EntityDataSession : IDataSession
    {
        private DbContext _dbContext;

        public EntityDataSession(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public DbSet<T> Set<T>() where T : class
        {
            return _dbContext.Set<T>();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
