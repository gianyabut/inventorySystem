using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.LunarMed.Web.Business.Interfaces
{
    public interface IGenericRepository<T>
    {
        T Get(int id);

        ICollection<T> GetAll();

        T Find(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] navigationProperties);

        List<T> List(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] navigationProperties);

        T Add(T entity);

        bool AddAll(IEnumerable<T> tList);

        T Update(T entity);

        bool Delete(T entity);

        bool DeleteAll(IEnumerable<T> tList);

        bool IsExists(T entity);
    }
}
