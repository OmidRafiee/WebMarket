namespace Shop.Models.Enums
{
    public interface IBaseRepository<T, TKey> where T : class
    {
        System.Linq.IQueryable < T > GetAll ();

        T GetRow ( TKey id );
    }

    public class BaseRepository<T, TKey> : IBaseRepository<T, TKey> where T : class
    {
        #region Implementation of IBaseRepository<T,TKey>

        public System.Linq.IQueryable < T > GetAll ()
        {
            return null;
        }

        public T GetRow ( TKey id )
        {
            return null;
        }

        #endregion
    }
}