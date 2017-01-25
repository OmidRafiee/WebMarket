using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Models.EntityModels
{
    public class City
    {
        public int Id { get; set; }

        [Display(Name = "نام شهر")]
        [Required(ErrorMessage = "نام شهر را وارد کنید", AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "نام شهر نباید بیشتر از 50 کاراکتر باشد")]
        public string Name { get; set; }

        [Display(Name = "نام استان")]
        [Required(ErrorMessage = "نام استان را وارد کنید", AllowEmptyStrings = false)]
       [ScaffoldColumn(false)]
        public int? StateId { get; set; }

        public virtual State State { get; set; }
        public ICollection<Reseller> Resellers { get; set; }
    }
}