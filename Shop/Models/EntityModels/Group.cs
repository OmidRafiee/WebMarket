using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Shop.Models.EntityModels
{
    public  class Group
    {
        [ScaffoldColumn(false)]
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "نام را وارد کنید", AllowEmptyStrings = false)]
        [DisplayName("گروه پدر")]
        [Display(Name = "گروه پدر")]
        [StringLength(50, ErrorMessage = "تعداد کاراکتر ها می بایست حداکثر 50 کاراکتر باشد")]
        public string Name { get; set; }

        [DisplayName("گروه پدر")]
        [Display(Name = "گروه پدر")]
        public int? ParentId { get; set; }

        public ICollection <Product> Products { get; set; }

       [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ParentId")]
        public virtual ICollection<Group> Groups { get; set; }
    }
}