using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shop.Models.EntityModels;


namespace Shop.Models.Repositories
{
    public class ResellerRepository  
    {
        //readonly DatabaseContext _db = new DatabaseContext();
        
        private readonly DatabaseContext _db = null;
        public ResellerRepository(DatabaseContext db)
        {
            _db = db;
        }

        //be ezaie har Factor k add mishavad yek bar anmaliat save ro anjam mide ..... 
        // bara inke n tedad ro yek dafe save konad baia autosave=false va metod joda baraie save benevisim
        public bool Add(Reseller reseller, bool autoSave = true)
        {
            try
            {
                _db.Resellers.Add(reseller);
                if (autoSave)
                    return Convert.ToBoolean(_db.SaveChanges());
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Update(Reseller reseller, bool autoSave = true)
        {
            try
            {
                _db.Resellers.Attach(reseller);
                _db.Entry(reseller).State = System.Data.Entity.EntityState.Modified;
                if (autoSave)
                    return Convert.ToBoolean(_db.SaveChanges());
                return false;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public bool Delete(Reseller reseller, bool autoSave = true)
        {
            try
            {
                _db.Entry(reseller).State = System.Data.Entity.EntityState.Deleted;
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
                
                var factor = _db.Resellers.Find(id);
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

        public Reseller Find(int id)
        {
            try
            {
                return _db.Resellers.Find(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        //predicate ye shart ro migirad va be where pas midahad
        public IQueryable<Reseller> Where(System.Linq.Expressions.Expression<Func<Reseller, bool>> predicate)
        {
            try
            {
                return _db.Resellers.Where(predicate);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public IQueryable<Reseller> Select()
        {
            try
            {
                //cast
                return _db.Resellers.AsQueryable();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IQueryable<TResult> Select<TResult>(System.Linq.Expressions.Expression<Func<Reseller, TResult>> selector)
        {
            try
            {
                //baraye select taki 
                //_db.Resellers.Select(p=>p.Name)

                //baraye select chantaii
                //_db.Resellers.Select ( p => new { Name = p.Name , ResellerName = p.ResellerName } ); 
                return _db.Resellers.Select(selector);
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
                if (_db.Resellers.Any())
                {
                    return _db.Resellers.OrderByDescending(p => p.Id).First().Id;
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