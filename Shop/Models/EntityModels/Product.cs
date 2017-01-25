using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace Shop.Models.EntityModels
{
    public class Product 
    {
        [ScaffoldColumn(false)]
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "نام محصول را وارد کنید")]
        [DisplayName("نام محصول")]
        [Display(Name = "نام محصول")]
        [StringLength(50, ErrorMessage = "تعداد کاراکتر ها می بایست حداکثر 50 کاراکتر باشد")]
        public string Name { get; set; }

        [Required(ErrorMessage = "قیمت محصول را وارد کنید", AllowEmptyStrings = false)]
        [DisplayName("قیمت محصول")]
        [Display(Name = "قیمت محصول")]
        [Range(0,int.MaxValue,ErrorMessage = "مقدار وارد شده نامعتبر است")]
        public int Price { get; set; }


       [Required(ErrorMessage = "قیمت محصول را وارد کنید", AllowEmptyStrings = false)]
        [DisplayName("قیمت  تخفیفی محصول")]
        [Display(Name = "قیمت تخفیفی محصول")]
        [Range(0, int.MaxValue, ErrorMessage = "مقدار وارد شده نامعتبر است")]
        public int OffPrice { get; set; }

         [Required(ErrorMessage = "آدرس محصول را وارد کنید")]
        [DisplayName("آدرس محصول")]
        [Display(Name = "آدرس محصول")]
        [System.Web.Mvc.Remote(action: "CheckUrl", controller: "Admin", HttpMethod = "POST", ErrorMessage = "(جهت ایجاد صفحه اختصاصی) آدرس وارد شده را بیشتر برای محصولی دیگر استفاده کرده اید  ")]
        //[DataType(DataType.Url,ErrorMessage = "آدرس محصول نامعتبر است")]
       [StringLength(100, ErrorMessage = "این فیلد باید حداکثر 100 کاراکتر باشد")]
        public string Url { get; set; }

        [DisplayName("کلمات کلیدی")]
        [Display(Name = "کلمات کلیدی")]
        [StringLength(300, ErrorMessage = "تعداد کاراکتر ها می بایست حداکثر 300 کاراکتر باشد")]
        public string KeyWord { get; set; }

        [DisplayName("توضیحات")]
        [Display(Name = "توضیحات")]
        [StringLength(500, ErrorMessage = "تعداد کاراکتر ها می بایست حداکثر 500 کاراکتر باشد")]
        public string Description { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "توضیحات محصول را وارد کنید", AllowEmptyStrings = false)]
        [DisplayName("توضیحات محصول")]
        [Display(Name = "توضیحات محصول")]
        [DataType(DataType.Html, ErrorMessage = "فرمت متن نا معتبر است")]
        public string Summery { get; set; }

        [DisplayName("برچسپ ها")]
        [Display(Name = "برچسپ ها")]
        [StringLength(200, ErrorMessage = "تعداد کاراکتر ها می بایست حداکثر 200 کاراکتر باشد")]
        public string Tag { get; set; }

        [DisplayName("تعداد Like")]
        [Display(Name = "تعداد Like")]
        [Range(0,int.MaxValue, ErrorMessage = "تعداد کاراکتر ها می بایست حداکثر 200 کاراکتر باشد")]
        public int Like { get; set; }

        [DisplayName("تعداد DisLike")]
        [Display(Name = "تعداد DisLike")]
        [Range(0, int.MaxValue, ErrorMessage = "مقدار وارد شده نامعتبر است")]
        public int DisLike { get; set; }

        [DisplayName("محصول فعال است")]
        [Display(Name = "محصول فعال است")]
        public bool Enable { get; set; }

        [DisplayName("گروه محصول")]
        [Display(Name = "گروه محصول")]
        [ScaffoldColumn(false)]
        public int GroupId { get; set; }

        [DisplayName("تصویر محصول")]
        [Display(Name = "تصویر محصول")]
        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }


        public virtual ICollection <FactorItem> FactorItems { get; set; }
        public virtual Group Group { get; set; }

        public System.Collections.IEnumerator GetEnumerator ()
        {
            return null;
        }
    }
}