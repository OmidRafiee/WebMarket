using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Models.EntityModels
{
    public class Factor
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        //[Required(ErrorMessage = "تاریخ خرید را وارد کنید", AllowEmptyStrings = false)]
        //[UIHint("PersianDatePicker")]
        [Display(Name = "تاریخ خرید")]
        [DisplayName("تاریخ خرید")]
        public DateTime BuyDate { get; set; }

        [Display(Name = "کد رهگیری ")]
        [DisplayName("کد رهگیری ")]
        public string FollowCode { get; set; }

        [Display(Name = "توضیحات")]
        [DisplayName("توضیحات")]
        [StringLength(400, ErrorMessage = "تعداد کاراکتر ها می بایست حداکثر 400 کاراکتر باشد")]
        public string Description { get; set; }

        [Display(Name = "خریدار")]
        [DisplayName("خریدار")]
        [ScaffoldColumn(false)]
        public int? UserId { get; set; }//shaid bedon login kharid konad b in dalil null

        [DisplayName("قیمت محصول")]
        [Display(Name = "قیمت محصول")]
        public decimal Price { get; set; }
        //if IsLogin == false then
        [Required(ErrorMessage = "نام را وارد کنید", AllowEmptyStrings = false)]
        [DisplayName(" نام خریدار ")]
        [Display(Name = "نام ")]
        [StringLength(50, ErrorMessage = "تعداد کاراکتر ها می بایست حداکثر 50 کاراکتر باشد")]
        public string Name { get; set; }

        [DisplayName("شماره موبایل")]
        [Display(Name = "شماره موبایل")]
        [RegularExpression(@"^0?9[123]\d{8}$", ErrorMessage = "شماره موبایل را بدرستی وارد کنید")]
        [StringLength(11, ErrorMessage = "این فیلد باید حداکثر 11 کاراکتر باشد")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "ایمیل خود را وارد کنید")]
        [Display(Name = "ایمیل")]
        [DisplayName("ایمیل")]
        [RegularExpression(@"^[_A-Za-z0-9-\+]+(\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\.[A-Za-z0-9-]+)*(\.[A-Za-z]{2,4})$", ErrorMessage = "ایمیل را بدرستی وارد کنید")]
        public string Email { get; set; }

        [Required(ErrorMessage = "آدرس خود را وارد کنید")]
        [Display(Name = "آدرس")]
        [DisplayName("آدرس")]
        [StringLength(400, ErrorMessage = "تعداد کاراکتر ها می بایست حداکثر 400 کاراکتر باشد")]
        public string Address { get; set; }

        [Display(Name = "کد پستی ")]
        [DisplayName("کد پستی ")]
        [StringLength(10, ErrorMessage = "کد پستی 10 رقمی را وارد کنید")]
        public string CodePosti { get; set; }

        [ScaffoldColumn(false)]
        public bool IsFinish { get; set; }

        public virtual User User { get; set; }
    }
}