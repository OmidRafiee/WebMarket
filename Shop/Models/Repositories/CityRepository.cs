using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shop.Models.EntityModels;


namespace Shop.Models.Repositories
{
    public class CityRepository  
    {
        //readonly DatabaseContext _db = new DatabaseContext();
        
        private readonly DatabaseContext _db = null;
        public CityRepository(DatabaseContext db)
        {
            _db = db;
        }

        //be ezaie har Factor k add mishavad yek bar anmaliat save ro anjam mide ..... 
        // bara inke n tedad ro yek dafe save konad baia autosave=false va metod joda baraie save benevisim
        public bool Add(City city, bool autoSave = true)
        {
            try
            {
                _db.Cities.Add(city);
                if (autoSave)
                    return Convert.ToBoolean(_db.SaveChanges());
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Update(City city, bool autoSave = true)
        {
            try
            {
                _db.Cities.Attach(city);
                _db.Entry(city).State = System.Data.Entity.EntityState.Modified;
                if (autoSave)
                    return Convert.ToBoolean(_db.SaveChanges());
                return false;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public bool Delete(City city, bool autoSave = true)
        {
            try
            {
                _db.Entry(city).State = System.Data.Entity.EntityState.Deleted;
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
                
                var factor = _db.Cities.Find(id);
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

        public City Find(int id)
        {
            try
            {
                return _db.Cities.Find(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        //predicate ye shart ro migirad va be where pas midahad
        public IQueryable<City> Where(System.Linq.Expressions.Expression<Func<City, bool>> predicate)
        {
            try
            {
                return _db.Cities.Where(predicate);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public IQueryable<City> Select()
        {
            try
            {
                //cast
                return _db.Cities.AsQueryable();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IQueryable<TResult> Select<TResult>(System.Linq.Expressions.Expression<Func<City, TResult>> selector)
        {
            try
            {
                //baraye select taki 
                //_db.Cities.Select(p=>p.Name)

                //baraye select chantaii
                //_db.Cities.Select ( p => new { Name = p.Name , CityName = p.CityName } ); 
                return _db.Cities.Select(selector);
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
                if (_db.Cities.Any())
                {
                    return _db.Cities.OrderByDescending(p => p.Id).First().Id;
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