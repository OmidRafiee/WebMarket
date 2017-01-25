using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shop.Models.EntityModels;


namespace Shop.Models.Repositories
{
    public class UserRepository
    {
        //readonly DatabaseContext _db = new DatabaseContext();

        private readonly DatabaseContext _db = null;
        public UserRepository(DatabaseContext db)
        {
            _db = db;
        }

        //be ezaie har user k add mishavad yek bar anmaliat save ro anjam mide ..... 
        // bara inke n tedad ro yek dafe save konad baia autosave=false va metod joda baraie save benevisim
        public bool Add(User user, bool autoSave = true)
        {
            try
            {
                
                        _db.Users.Add(user);
                        if (autoSave)
                           return Convert.ToBoolean(_db.SaveChanges());
                   return false;
            }
            catch(Exception)
            {

                return false;
            }
        }

        public bool Update(User user, bool autoSave = true)
        {
            try
            {
                _db.Users.Attach(user);
                _db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                if (autoSave)
                    return Convert.ToBoolean(_db.SaveChanges());
                return false;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public bool Exist(string useName, string password)
        {
            try
            {
                return _db.Users.Any(p => p.UserName == useName && p.Password == password);
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Delete(User user, bool autoSave = true)
        {
            try
            {
                _db.Entry(user).State = System.Data.Entity.EntityState.Deleted;
                if (autoSave)
                    return Convert.ToBoolean(_db.SaveChanges());
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Delete(int id, bool autoSave = true)
        {
            try
            {
                ////way1
                // var deletUser = _db.Users.Single(p => p.Id == id);
                // _db.Users.Remove(deletUser);
                // return Convert.ToBoolean(_db.SaveChanges());

                //way2
                var user = _db.Users.Find(id);
                _db.Entry(user).State = System.Data.Entity.EntityState.Deleted;
                if (autoSave)
                    return Convert.ToBoolean(_db.SaveChanges());
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public User Find(int id)
        {
            try
            {
                return _db.Users.Find(id);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public User Find(string userName)
        {
            try
            {
                return _db.Users.Find(userName);
            }
            catch (Exception)
            {
                return null;
            }
        }
        //predicate ye shart ro migirad va be where pas midahad
        public IQueryable<User> Where(System.Linq.Expressions.Expression<Func<User, bool>> predicate)
        {
            try
            {
                return _db.Users.Where(predicate);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public IQueryable<User> Select()
        {
            try
            {
                //cast
                return _db.Users.AsQueryable();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IQueryable<TResult> Select<TResult>(System.Linq.Expressions.Expression<Func<User, TResult>> selector)
        {
            try
            {
                //baraye select taki 
                //_db.Users.Select(p=>p.Name)

                //baraye select chantaii
                //_db.Users.Select ( p => new { Name = p.Name , UserName = p.UserName } ); 
                return _db.Users.Select(selector);
            }
            catch (Exception)
            {

                return null;
            }
        }


        public int GetLastIdentity()
        {
            try
            {
                if (_db.Users.Any())
                {
                    return _db.Users.OrderByDescending(p => p.Id).First().Id;
                }
                return 0;
            }
            catch (Exception)
            {

                return -1;
            }
        }

        public int Save()
        {
            try
            {
                return _db.SaveChanges();
            }
            catch (Exception)
            {
                return -1;
            }
        }

    }
}