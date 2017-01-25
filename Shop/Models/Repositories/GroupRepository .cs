using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shop.Models.EntityModels;


namespace Shop.Models.Repositories
{
    public class GroupRepository  
    {
        //readonly DatabaseContext _db = new DatabaseContext();
        
        private readonly DatabaseContext _db = null;
        public GroupRepository(DatabaseContext db)
        {
            _db = db;
        }

        //be ezaie har group k add mishavad yek bar anmaliat save ro anjam mide ..... 
        // bara inke n tedad ro yek dafe save konad baia autosave=false va metod joda baraie save benevisim
        public bool Add(Group group, bool autoSave = true)
        {
            try
            {
                _db.Groups.Add(group);
                if (autoSave)
                    return Convert.ToBoolean(_db.SaveChanges());
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Update(Group group, bool autoSave = true)
        {
            try
            {
                _db.Groups.Attach(group);
                _db.Entry(group).State = System.Data.Entity.EntityState.Modified;
                if (autoSave)
                    return Convert.ToBoolean(_db.SaveChanges());
                return false;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public bool Delete(Group group, bool autoSave = true)
        {
            try
            {
                _db.Entry(group).State = System.Data.Entity.EntityState.Deleted;
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
                // var deletgroup = _db.groups.Single(p => p.Id == id);
                // _db.groups.Remove(deletgroup);
                // return Convert.ToBoolean(_db.SaveChanges());

                //way2
                var group = _db.Groups.Find(id);
                _db.Entry(group).State = System.Data.Entity.EntityState.Deleted;
                if (autoSave)
                    return Convert.ToBoolean(_db.SaveChanges());
                return false;
            }
            catch (Exception)
            {
                
                return false;
            }
        }

        public Group Find(int id)
        {
            try
            {
                return _db.Groups.Find(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        //predicate ye shart ro migirad va be where pas midahad
        public IQueryable<Group> Where(System.Linq.Expressions.Expression<Func<Group, bool>> predicate)
        {
            try
            {
                return _db.Groups.Where(predicate);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public IQueryable<Group> Select()
        {
            try
            {
                //cast
                return _db.Groups.AsQueryable();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IQueryable<TResult> Select<TResult>(System.Linq.Expressions.Expression<Func<Group, TResult>> selector)
        {
            try
            {
                //baraye select taki 
                //_db.groups.Select(p=>p.Name)

                //baraye select chantaii
                //_db.groups.Select ( p => new { Name = p.Name , groupName = p.groupName } ); 
                return _db.Groups.Select ( selector );
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
                if (_db.Groups.Any())
                {
                    return _db.Groups.OrderByDescending(p => p.Id).First().Id;
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