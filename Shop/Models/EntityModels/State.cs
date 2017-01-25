using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Models.EntityModels
{
    public class State
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "نام استان")]
        [Required(ErrorMessage = "نام استان را وارد کنید",AllowEmptyStrings = false)]
        [StringLength(50,ErrorMessage = "نام استان نباید بیشتر از 50 کاراکتر باشد")]
        public string Name { get; set; }

        public ICollection<City> Cities { get; set; }
        public ICollection<Reseller> Resellers { get; set; }
    }
}