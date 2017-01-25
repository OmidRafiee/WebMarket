using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shop.Models.EntityModels;


namespace Shop.Models.Repositories
{
    public class ProductRepository
    {
        //readonly DatabaseContext _db = new DatabaseContext();

        private readonly DatabaseContext _db = null;

        public ProductRepository ( DatabaseContext db )
        {
            _db = db;
        }

        //be ezaie har product k add mishavad yek bar anmaliat save ro anjam mide ..... 
        // bara inke n tedad ro yek dafe save konad baia autosave=false va metod joda baraie save benevisim
        public bool Add ( Product product ,
                          bool autoSave = true )
        {
            try
            {
                _db.Products.Add ( product );
                if ( autoSave )
                    return Convert.ToBoolean ( _db.SaveChanges () );
                return false;
            }
            catch ( Exception )
            {

                return false;
            }
        }

        public bool Update ( Product product ,
                             string path ,
                             bool autoSave = true )
        {
            try
            {
                if ( path != null )
                {
                    product.Image = path;
                }
                _db.Products.Attach ( product );
                _db.Entry ( product ).State = System.Data.Entity.EntityState.Modified;
                if ( autoSave )
                    return Convert.ToBoolean ( _db.SaveChanges () );
                return false;
            }
            catch ( Exception )
            {

                return false;
            }

        }

        public bool Delete ( Product product ,
                             bool autoSave = true )
        {
            try
            {
                _db.Entry ( product ).State = System.Data.Entity.EntityState.Deleted;
                if ( autoSave )
                    return Convert.ToBoolean ( _db.SaveChanges () );
                return false;
            }
            catch ( Exception )
            {

                return false;
            }
        }

        public bool Delete ( int id ,
                             bool autoSave = true )
        {
            try
            {
                ////way1
                // var deletproduct = _db.products.Single(p => p.Id == id);
                // _db.products.Remove(deletproduct);
                // return Convert.ToBoolean(_db.SaveChanges());

                //way2
                var product = _db.Products.Find ( id );
                _db.Entry ( product ).State = System.Data.Entity.EntityState.Deleted;
                if ( autoSave )
                    if ( Convert.ToBoolean ( _db.SaveChanges () ) )
                    {
                        var path = AppDomain.CurrentDomain.BaseDirectory + "Files\\UploadImages\\" + product.Image;
                        try
                        {
                            if ( System.IO.File.Exists ( path ) )
                            {
                                System.IO.File.Delete ( path );
                            }
                        }
                        catch
                        {
                            // ignored
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                return false;
            }
            catch ( Exception )
            {

                return false;
            }
        }

        public Product Find ( int id )
        {
            try
            {
                return _db.Products.Find ( id );
            }
            catch ( Exception )
            {
                return null;
            }
        }

        //predicate ye shart ro migirad va be where pas midahad
        public IEnumerable<Product> Where(System.Linq.Expressions.Expression<Func<Product, bool>> predicate)
        {
            try
            {
                return _db.Products.Where ( predicate );
            }
            catch ( Exception )
            {

                return null;
            }
        }
       
    public IEnumerable<Product> Select()
        {
            try
            {
                //cast
                return _db.Products.AsQueryable();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<TResult> Select<TResult>(System.Linq.Expressions.Expression<Func<Product, TResult>> selector)
        {
            try
            {
                //baraye select taki 
                //_db.products.Select(p=>p.Name)

                //baraye select chantaii
                //_db.products.Select ( p => new { Name = p.Name , productName = p.productName } ); 
                return _db.Products.Select(selector);
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
                if (_db.Products.Any())
                {
                    return _db.Products.OrderByDescending(p => p.Id).First().Id;
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