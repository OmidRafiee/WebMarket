using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shop.Models.EntityModels;


namespace Shop.Models.Repositories
{
    public class FactorItemRepository  
    {
        //readonly DatabaseContext _db = new DatabaseContext();
        
        private readonly DatabaseContext _db = null;
        public FactorItemRepository(DatabaseContext db)
        {
            _db = db;
        }

        //be ezaie har Factor k add mishavad yek bar anmaliat save ro anjam mide ..... 
        // bara inke n tedad ro yek dafe save konad baia autosave=false va metod joda baraie save benevisim
        public bool Add(FactorItem factor, bool autoSave = true)
        {
            try
            {
                _db.FactorItems.Add(factor);
                if (autoSave)
                    return Convert.ToBoolean(_db.SaveChanges());
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Update(FactorItem factor, bool autoSave = true)
        {
            try
            {
                _db.FactorItems.Attach(factor);
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

        public bool Delete(FactorItem factor, bool autoSave = true)
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
                
                var factor = _db.FactorItems.Find(id);
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

        public FactorItem Find(int id)
        {
            try
            {
                return _db.FactorItems.Find(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        //predicate ye shart ro migirad va be where pas midahad
        public IQueryable<FactorItem> Where(System.Linq.Expressions.Expression<Func<FactorItem, bool>> predicate)
        {
            try
            {
                return _db.FactorItems.Where(predicate);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public IQueryable<FactorItem> Select()
        {
            try
            {
                //cast
                return _db.FactorItems.AsQueryable();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IQueryable<TResult> Select<TResult>(System.Linq.Expressions.Expression<Func<FactorItem, TResult>> selector)
        {
            try
            {
                //baraye select taki 
                //_db.FactorItems.Select(p=>p.Name)

                //baraye select chantaii
                //_db.FactorItems.Select ( p => new { Name = p.Name , FactorItemName = p.FactorItemName } ); 
                return _db.FactorItems.Select(selector);
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
                if (_db.FactorItems.Any())
                {
                    return _db.FactorItems.OrderByDescending(p => p.Id).First().Id;
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