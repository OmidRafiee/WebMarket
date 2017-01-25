using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace Shop
{
    public class BundleConfig
    {
        public static void RegisterBundle(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jqueryAjax").Include("~/Scripts/jquery.unobtrusive-ajax.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryVal").Include("~/Scripts/jquery.validate.unobtrusive.min.js",
                 "~/Scripts/jquery.validate.min.js", "~/Scripts/jquery-3.1.0.min.js"));
        }
    }
}
