namespace BL.Interfaces;

public interface IGenericRepository <T>
{
    Task<IEnumerable<T>> GetAll ();
    Task<T> GetById (int id);
    Task Add (T entity);
    Task<bool> Exist (int id);
   void Update (T entity);
   void Delete (T entity);
}
