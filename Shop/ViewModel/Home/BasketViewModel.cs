using Shop.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.ViewModel.Home
{
    public class BasketViewModel
    {
        public Product Product { get; set; }
        public int Count { get; set; }

    }
}