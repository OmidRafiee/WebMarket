using Shop.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.ViewModel.Home
{
    public class HomeIndexViewModel
    {
        public IEnumerable<Group> Groups { get; set; }
        public IEnumerable<Product> BestSellerProducts { get; set; }
        public IEnumerable<Product> OffProducts { get; set; }
        public IEnumerable<Product> Products { get; set; }
        
    }
}