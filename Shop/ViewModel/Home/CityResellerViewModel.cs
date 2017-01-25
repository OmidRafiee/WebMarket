using Shop.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.ViewModel.Home
{
    public class CityResellerViewModel
    {
        public IEnumerable <City> Cities { get; set; }
        public  Reseller Reseller { get; set; }
    }
}