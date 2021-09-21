using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserRegistirationApp.Data.Abstract
{
    public interface IGenericRepository<T>
    {
        ICollection<T> GetAll();
        bool Create(T entity);
        bool Update(T entity);
        bool Delete(int id);
        T GetById(int id);
    }
}
