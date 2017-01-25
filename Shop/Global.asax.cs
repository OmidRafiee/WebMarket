using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using Shop.Models.EntityModels;
using System.Web.Security;
using Shop.Models.Repositories;
using System.Security.Principal;

namespace Shop
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DatabaseContext>()); //just for update db 


            //for create db before run program
            using (var context = new DatabaseContext())
            {
                context.Database.Initialize(force: true);
            }
            
        }


        //for check  FormsAuthentication in role
        protected void FormsAuthentication_OnAuthenticate(Object sender, FormsAuthenticationEventArgs e)
        {
            if (FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    var formsAuthenticationTicket = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value);
                    if ( formsAuthenticationTicket != null )
                    {
                        string username =formsAuthenticationTicket.Name;
                        var blUser = new UserRepository( new DatabaseContext());
                        var singleOrDefault = blUser.Where(p => p.UserName == username).FirstOrDefault();
                        if (singleOrDefault != null && singleOrDefault.Role!=null)
                        {
                            string roles = singleOrDefault.Role;
                            e.User = new GenericPrincipal(new System.Security.Principal.GenericIdentity(username),roles.Split(','));
                        }
                    }
                }
            }
        }
    }
}
