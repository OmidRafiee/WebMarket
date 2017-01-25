using Shop.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.ViewModel.Admin
{
    public class AddResellerViewModel
    {
        public IEnumerable <State> States { get; set; }
        public  Reseller Reseller { get; set; }
    }
}