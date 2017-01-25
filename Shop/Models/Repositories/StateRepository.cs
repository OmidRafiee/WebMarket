using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shop.Models.EntityModels;


namespace Shop.Models.Repositories
{
    public class StateRepository  
    {
        //readonly DatabaseContext _db = new DatabaseContext();
        
        private readonly DatabaseContext _db = null;
        public StateRepository(DatabaseContext db)
        {
            _db = db;
        }

        //be ezaie har Factor k add mishavad yek bar anmaliat save ro anjam mide ..... 
        // bara inke n tedad ro yek dafe save konad baia autosave=false va metod joda baraie save benevisim
        public bool Add(State state, bool autoSave = true)
        {
            try
            {
                _db.States.Add(state);
                if (autoSave)
                    return Convert.ToBoolean(_db.SaveChanges());
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Update(State state, bool autoSave = true)
        {
            try
            {
                _db.States.Attach(state);
                _db.Entry(state).State = System.Data.Entity.EntityState.Modified;
                if (autoSave)
                    return Convert.ToBoolean(_db.SaveChanges());
                return false;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public bool Delete(State state, bool autoSave = true)
        {
            try
            {
                _db.Entry(state).State = System.Data.Entity.EntityState.Deleted;
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
                
                var factor = _db.States.Find(id);
                _db.Entry(factor).State = System.Data.Entity.EntityState.Deleted;
                if (autoSave)
                    return Convert.ToBoolean(_db.SaveChanges());
                return false;
            }
            catch (Exception)
            {
                
                return false;
            }
        }

        public State Find(int id)
        {
            try
            {
                return _db.States.Find(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        //predicate ye shart ro migirad va be where pas midahad
        public IQueryable<State> Where(System.Linq.Expressions.Expression<Func<State, bool>> predicate)
        {
            try
            {
                return _db.States.Where(predicate);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public IQueryable<State> Select()
        {
            try
            {
                //cast
                return _db.States.AsQueryable();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IQueryable<TResult> Select<TResult>(System.Linq.Expressions.Expression<Func<State, TResult>> selector)
        {
            try
            {
                //baraye select taki 
                //_db.States.Select(p=>p.Name)

                //baraye select chantaii
                //_db.States.Select ( p => new { Name = p.Name , StateName = p.StateName } ); 
                return _db.States.Select(selector);
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
                if (_db.States.Any())
                {
                    return _db.States.OrderByDescending(p => p.Id).First().Id;
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