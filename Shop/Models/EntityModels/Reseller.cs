using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Models.EntityModels
{
    public class Reseller
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "نام نمایندگی")]
        [Required(ErrorMessage = "نام نمایندگی را وارد کنید", AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "نام نمایندگی نباید بیشتر از 50 کاراکتر باشد")]
        public string Name { get; set; }

        [Display(Name = "نام استان")]
        //[Required(ErrorMessage = "نام استان را وارد کنید", AllowEmptyStrings = false)]
        public int? StateId { get; set; }

        [Display(Name = "نام شهر")]
        public int? CityId { get; set; }

        [Display(Name = "آدرس نمایندگی")]
        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessage = "آدرس نمایندگی نباید بیشتر از 500 کاراکتر باشد")]
        public string Address { get; set; }

        public virtual State State { get; set; }
        public virtual City City { get; set; }
    }
}