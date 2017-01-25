using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Shop.Models.EntityModels
{
    public class User
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "نام را وارد کنید",AllowEmptyStrings = false)]
        [DisplayName("نام ")]
        [Display(Name = "نام ")]
        [StringLength(50,ErrorMessage = "تعداد کاراکتر ها می بایست حداکثر 50 کاراکتر باشد")]
        public string Name { get; set; }

        [Required(ErrorMessage = " نام خانوادگی را وارد کنید", AllowEmptyStrings = false)]
        [DisplayName("نام خانوادگی ")]
        [Display(Name = " نام خانوادگی")]
        [StringLength(50, ErrorMessage = "تعداد کاراکتر ها می بایست حداکثر 50 کاراکتر باشد")]
        public string Family { get; set; }

        [Required(ErrorMessage = "ایمیل خود را وارد کنید")]
        [System.Web.Mvc.Remote(action: "CheckUserName",controller: "Home",HttpMethod = "POST",
                               ErrorMessage = "(جهت لاگین) ایمیل وارد شده هم اکنون توسط یکی از کاربران مورد استفاده است ")]
        [Display(Name = "ایمیل (نام کاربری)")]
        [DisplayName("ایمیل (نام کاربری)")]
        [RegularExpression(@"^[_A-Za-z0-9-\+]+(\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\.[A-Za-z0-9-]+)*(\.[A-Za-z]{2,4})$", ErrorMessage = "ایمیل را بدرستی وارد کنید")]
        [StringLength(50, ErrorMessage = "تعداد کاراکتر ها می بایست حداکثر 50 کاراکتر باشد")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "رمز عبور خود را وارد کنید")]
        [DisplayName("رمز عبور")]
        [Display(Name = "رمز عبور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "رمز عبور خود را وارد کنید")]
        [DisplayName("تکرار رمز عبور")]
        [Display(Name = "تکرار رمز عبور")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [Compare("Password", ErrorMessage = "عدم تشابع رمز عبور های وارد شده")]
        public string ConfirmPassword { get; set; }

        [UIHint("PersianDatePicker")]
        [Display(Name = "تاریخ تولد")]
        [DisplayName("تاریخ تولد")]
        public DateTime? BirthDate { get; set; }

        [DisplayName("شماره موبایل")]
        [Display(Name = "شماره موبایل")]
        [RegularExpression(@"^0?9[123]\d{8}$", ErrorMessage = "شماره موبایل را بدرستی وارد کنید")]
        [StringLength(11, ErrorMessage = "این فیلد باید حداکثر 11 کاراکتر باشد")]
        public string Mobile { get; set; }

        [Display(Name = "شماره تماس")]
        [DisplayName("شماره تماس")]
        [RegularExpression(@"^0\d{10}$", ErrorMessage = "شماره تلفن را بدرستی وارد کنید")]
        [StringLength(11, ErrorMessage = "این فیلد باید حداکثر 11 کاراکتر باشد")]
        public string Tell { get; set; }

        [DisplayName("جنسیت")]
        [Display(Name = "جنسیت")]
        public bool Gender { get; set; }

       
        [Display(Name = "حاصل جمع")]
        [DisplayName("حاصل جمع")]
        [Required(ErrorMessage = "لطفا حاصل جمع را وارد کنید")]
        public string Captcha { get; set; }

        [ScaffoldColumn(false)]
        [DisplayName("وضعیت")]
        [Display(Name = "وضعیت")]
        public string Role { get; set; }

        public virtual ICollection<Factor> Factors{ get; set; }
        public virtual ICollection<FactorItem> FactorItems { get; set; }
    }
 
}
