using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Models.EntityModels
{
    public class FactorItem
    {
        public int Id { get; set; }
        public int FactorId { get; set; }
        public int ProductId { get; set; }
        public byte Qty  { get; set; }//count
  

        
        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
    }
}