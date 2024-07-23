using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRestAPI.DAL.Repository.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        T GetById(int id);
        IEnumerable<T> Get(int page = 1, int pageSize = 10);
        IEnumerable<T> Get(Func<T, bool> predicate, int page = 1, int pageSize = 10);
        T Save(T entity);
        void Delete(int id);

    }
}
