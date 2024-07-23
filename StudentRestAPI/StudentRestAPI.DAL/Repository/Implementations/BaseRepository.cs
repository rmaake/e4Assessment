using Microsoft.EntityFrameworkCore;
using StudentRestAPI.DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRestAPI.DAL.Repository.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DbContext _dbContext;
        public BaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(int id)
        {
            T entity = GetById(id);

            if (entity == null)
                return;

            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public IEnumerable<T> Get(int page = 1, int pageSize = 10)
        {
            return _dbContext.Set<T>().Skip(pageSize * (page - 1)).Take(page).ToList();
        }

        public IEnumerable<T> Get(Func<T, bool> predicate, int page = 1, int pageSize = 10)
        {
            return _dbContext.Set<T>().Where(predicate).Skip(pageSize * (page - 1)).Take(page).ToList();
        }

        public T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public T Save(T entity)
        {
            if (entity == null)
                return null;
            if (_dbContext.Entry<T>(entity).State == EntityState.Detached)
            {
                _dbContext.Set<T>().Add(entity);
            }
            else
            {
                _dbContext.Set<T>().Update(entity);
            }
            _dbContext.SaveChanges();
            _dbContext.Entry(entity).Reload();
            return entity;
        }
    }
}
