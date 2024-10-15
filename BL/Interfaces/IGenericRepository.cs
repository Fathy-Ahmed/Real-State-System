using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IGenericRepository <T>
    {
        Task<IEnumerable<T>> GetAll ();
        Task<T> GetById (int id);
        Task Add (T entity);

       void Update (T entity);
       void Delete (T entity);
    }
}
