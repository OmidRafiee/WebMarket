using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shop.Models.EntityModels;


namespace Shop.Models.Repositories
{
    public class FactorRepository  
    {
        //readonly DatabaseContext _db = new DatabaseContext();
        
        private readonly DatabaseContext _db = null;
        public FactorRepository(DatabaseContext db)
        {
            _db = db;
        }

        //be ezaie har Factor k add mishavad yek bar anmaliat save ro anjam mide ..... 
        // bara inke n tedad ro yek dafe save konad baia autosave=false va metod joda baraie save benevisim
        public bool Add(Factor factor, bool autoSave = true)
        {
            try
            {
                _db.Factors.Add(factor);
                if (autoSave)
                    return Convert.ToBoolean(_db.SaveChanges());
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Update(Factor factor, bool autoSave = true)
        {
            try
            {
                _db.Factors.Attach(factor);
                _db.Entry(factor).State = System.Data.Entity.EntityState.Modified;
                if (autoSave)
                    return Convert.ToBoolean(_db.SaveChanges());
                return false;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public bool Delete(Factor factor, bool autoSave = true)
        {
            try
            {
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

        public bool Delete(int id, bool autoSave = true)
        {
            try
            {
                
                var factor = _db.Factors.Find(id);
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

        public Factor Find(int id)
        {
            try
            {
                return _db.Factors.Find(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        //predicate ye shart ro migirad va be where pas midahad
        public IQueryable<Factor> Where(System.Linq.Expressions.Expression<Func<Factor, bool>> predicate)
        {
            try
            {
                return _db.Factors.Where(predicate);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public IQueryable<Factor> Select()
        {
            try
            {
                //cast
                return _db.Factors.AsQueryable();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IQueryable<TResult> Select<TResult>(System.Linq.Expressions.Expression<Func<Factor, TResult>> selector)
        {
            try
            {
                //baraye select taki 
                //_db.Factors.Select(p=>p.Name)

                //baraye select chantaii
                //_db.Factors.Select ( p => new { Name = p.Name , FactorName = p.FactorName } ); 
                return _db.Factors.Select(selector);
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
                if (_db.Factors.Any())
                {
                    return _db.Factors.OrderByDescending(p => p.Id).First().Id;
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