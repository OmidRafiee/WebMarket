using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Shop.Models.EntityModels
{
    public class DatabaseContext : System.Data.Entity.DbContext
    {
       
        public System.Data.Entity.DbSet<Group> Groups { set; get; }
        public System.Data.Entity.DbSet<Message> Messages { get; set; }
        public System.Data.Entity.DbSet<FactorItem> FactorItems { get; set; }
        public System.Data.Entity.DbSet<Product> Products { get; set; }
        public System.Data.Entity.DbSet<Factor> Factors { set; get; }
        public System.Data.Entity.DbSet<Setting> Settings { get; set; }
        public System.Data.Entity.DbSet<User> Users { set; get; }
        public System.Data.Entity.DbSet<Reseller> Resellers { set; get; }
        public System.Data.Entity.DbSet<City> Cities { get; set; }
        public System.Data.Entity.DbSet<State> States { set; get; }

       
    }

    //bedon niaz
    public class DatabaseContextInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        public DatabaseContextInitializer()
        {

        }

    }


}