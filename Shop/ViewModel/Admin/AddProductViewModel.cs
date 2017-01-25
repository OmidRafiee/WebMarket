using Shop.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.ViewModel.Admin
{
    public class AddProductViewModel
    {
        public IEnumerable<Group> Groups { get; set; }
        public Product Product { get; set; }
    }
}