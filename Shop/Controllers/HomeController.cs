using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Models.EntityModels;
using Shop.Models.Repositories;
using System.Web.UI;
using System.IO;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        readonly GroupRepository _blGroup = new GroupRepository(new DatabaseContext());
        readonly ProductRepository _blProduct = new ProductRepository(new DatabaseContext());
        readonly FactorRepository _blFactor = new FactorRepository(new DatabaseContext());
        readonly FactorItemRepository _blFactorItem = new FactorItemRepository(new DatabaseContext());
        readonly StateRepository _blState = new StateRepository(new DatabaseContext());
        readonly ResellerRepository _blReseller = new ResellerRepository(new DatabaseContext());
        readonly CityRepository _blCity = new CityRepository(new DatabaseContext());
        readonly UserRepository _blUser = new UserRepository(new DatabaseContext());

      


        [HttpGet]
        // GET: Home
        public ActionResult Index()
        {
            //using (var db = new DatabaseContext())
            //{
            //    db.Users.Add(new User()
            //                   {
            //                       Name = "Omid",
            //                       Family = "Rafiee",
            //                       UserName = "omid.green@gmail.com",
            //                       Password = "12345",
            //                       Tell = "02122546781",
            //                       Mobile = "09361541257",
            //                       BirthDate = DateTime.Now.Date
            //                   });
            //    db.SaveChanges();
            //}


            var model = new ViewModel.Home.HomeIndexViewModel();
            model.Groups = _blGroup.Select();

          //  model.OffProducts = _blProduct.Where(p => p.FactorItems.Count(f => f.ProductId));

            model.Products = _blProduct.Select().OrderByDescending(p => p.Id).Take(8);
            model.BestSellerProducts = _blProduct.Select().OrderBy(p => p.FactorItems.Count>1).Take(4);
            model.OffProducts = _blProduct.Select().OrderByDescending(p => p.OffPrice).Take(6);
            model.Groups = _blGroup.Where(p => p.ParentId == null);
            return View(model);
        }


        

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (Session["Captcha"] == null || Session["Captcha"].ToString() != user.Captcha)
            {
                ModelState.AddModelError("Captcha", "مجموع  وارد شده اشتباه است");
            }

            if (ModelState.IsValid)
            {
                if (user.BirthDate != null)
                {
                    user.BirthDate = user.BirthDate.Value.ToMiladiDate();
                    user.Role = "User";//Add default rol
                }


                if (_blUser.Add(user))
                {
                    return MessageBox.Show("اطلاعات با موفقیت ثبت شد", MessageType.Success);
                }
                return MessageBox.Show("در ثبت اطلاعات خطا رخ داد !", MessageType.Error);
            }
            else
            {
                // when client validation disbale just active sever validation
                return MessageBox.Show(ModelState.GetErrors(), MessageType.Warning);
            }
        }

        public ActionResult CaptchaImage(string prefix, bool noisy = true)
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            //generate new question
            int a = rand.Next(10, 99);
            int b = rand.Next(0, 9);
            var captcha = string.Format("{0} + {1} = ?", a, b);

            //store answer
            Session["Captcha" + prefix] = a + b;

            //image stream
            FileContentResult img = null;

            using (var mem = new MemoryStream())
            using (var bmp = new Bitmap(130, 30))
            using (var gfx = Graphics.FromImage((Image)bmp))
            {
                gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));

                //add noise
                if (noisy)
                {
                    int i, r, x, y;
                    var pen = new Pen(Color.Yellow);
                    for (i = 1; i < 10; i++)
                    {
                        pen.Color = Color.FromArgb(
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)));

                        r = rand.Next(0, (130 / 3));
                        x = rand.Next(0, 130);
                        y = rand.Next(0, 30);

                        gfx.DrawEllipse(pen, x - r, y - r, r, r);
                    }
                }

                //add question
                gfx.DrawString(captcha, new Font("Tahoma", 15), Brushes.Gray, 2, 3);

                //render as Jpeg
                bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
                img = this.File(mem.GetBuffer(), "image/Jpeg");
            }

            return img;
        }

        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult CheckUserName(string userName)
        {
            var colection = _blUser.Select(p => p.UserName);
            foreach (var item in colection)
            {
                if (item.ToLowerInvariant() == userName.ToLowerInvariant())
                {
                    return Json(false);
                }
            }
            return Json(true);
        }

      
        [HttpPost]
        public ActionResult AddToCart(int id, byte count)
        {
            try
            {

                if (Request.Cookies.AllKeys.Contains("Cart_" + id))
                {
                    //edit cookie
                    var httpCookie = Request.Cookies["Cart_" + id];
                    if (httpCookie != null)
                    {
                        var cookie = new HttpCookie("Cart_" + id, count.ToString());
                        cookie.Expires = DateTime.Now.AddDays(10);
                        cookie.HttpOnly = true;
                        Response.Cookies.Set(cookie);
                    }
                }
                else
                {
                    //Add new cookie
                    var cookie = new HttpCookie("Cart_" + id, count.ToString());
                    cookie.Expires = DateTime.Now.AddDays(10);
                    cookie.HttpOnly = true;
                    Response.Cookies.Add(cookie);
                    TotalPrice();
                }

                if (count == 1)
                {
                    
                    return Json(new JsonData()
                    {
                        Success = true,
                        Script = MessageBox.Show("کالا به سبد خرید اضافه شد",
                                                   MessageType.Success).Script,
                        Html =""
                    });
                    
                }
                else
                {
                    
                    return Json(new JsonData()
                    {
                        Success = true,
                        Script = MessageBox.Show("تعداد کالا سبد خرید ویرایش شد",
                                                   MessageType.Success).Script,
                        Html = "(" + "لیست خرید (" + CartCount()
                    });
                }

            }
            catch (Exception)
            {

                return Json(new JsonData()
                {
                    Success = false,
                    Script = MessageBox.Show("کالا به سبد خرید اضافه نشد",
                                               MessageType.Error).Script,
                    Html = ""
                });
            }
        }

        public ActionResult RemoveCart(int id)
        {
            try
            {
                if (Request.Cookies.AllKeys.Contains("Cart_" + id))
                {
                    var httpCookie = Response.Cookies["Cart_" + id];
                    if (httpCookie != null)
                        httpCookie.Expires = DateTime.Now.AddDays(-1);
                    Request.Cookies.Remove("Cart_" + id);

                    return Json(new JsonData()
                    {
                        Success = true,
                        Script = MessageBox.Show("کالا از سبد خرید حذف شد",
                                                   MessageType.Success).Script,
                        Html = "(" + "لیست خرید (" + CartCount()
                    });
                }
                else
                {
                    return Json(new JsonData()
                    {
                        Success = false,
                        Script = MessageBox.Show("این کالا در سبد خرید شما وجود ندارد",
                                                   MessageType.Warning).Script,
                        Html = "(" + "لیست خرید (" + CartCount()
                    });
                }
            }
            catch (Exception)
            {
                return Json(new JsonData()
                {
                    Success = false,
                    Script = MessageBox.Show("کالا از سبد خرید حذف نشد",
                                               MessageType.Error).Script,
                    Html = ""
                });
            }

        }

        public string CartCount()
        {
            var lstCookie = new List<HttpCookie>();
            for (int i = 0; i < Request.Cookies.Count; i++)
            {
                if (lstCookie.Any(p => p.Name == Request.Cookies[i].Name) == false)
                    lstCookie.Add(Request.Cookies[i]);
            }
            var cartCount = lstCookie.Count(p => p.Name.StartsWith("Cart_"));
            return cartCount.ToString();
        }

        public PartialViewResult TotalPrice()
        {
            var lstBasket = new List<ViewModel.Home.BasketViewModel>();
            var lstCookie = new List<HttpCookie>();
            for (int i = 0; i < Request.Cookies.Count; i++)
            {
                if (lstCookie.Any(p => p.Name == Request.Cookies[i].Name) == false)
                {
                    lstCookie.Add(Request.Cookies[i]);
                }
            }

            foreach (var item in lstCookie.Where(p => p.Name.StartsWith("Cart_")))
            {
                lstBasket.Add(new ViewModel.Home.BasketViewModel
                {
                    Product = _blProduct.Find(Convert.ToInt32(item.Name.Substring(5))),
                    Count = Convert.ToInt32(item.Value)
                });
            }
            return PartialView("_Basket", lstBasket);
        }

        [HttpGet]
        public ActionResult Basket()
        {
            var lstBasket = new List<ViewModel.Home.BasketViewModel>();
            var lstCookie = new List<HttpCookie>();
            for (int i = 0; i < Request.Cookies.Count; i++)
            {
                if (lstCookie.Any(p => p.Name == Request.Cookies[i].Name) == false)
                {
                    lstCookie.Add(Request.Cookies[i]);
                }
            }

            foreach (var item in lstCookie.Where(p => p.Name.StartsWith("Cart_")))
            {
                lstBasket.Add(new ViewModel.Home.BasketViewModel
                                {
                                    Product = _blProduct.Find(Convert.ToInt32(item.Name.Substring(5))),
                                    Count = Convert.ToInt32(item.Value)
                                });
            }
            return View(lstBasket);
        }

        [HttpGet]
        public ActionResult Buy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Buy(Factor factor)
        {
            if (ModelState.IsValid)
            {
                var lstBasket = new List<ViewModel.Home.BasketViewModel>();
                var lstCookie = new List<HttpCookie>();
                for (int i = 0; i < Request.Cookies.Count; i++)
                {
                    if (lstCookie.Any(p => p.Name == Request.Cookies[i].Name) == false)
                    {
                        lstCookie.Add(Request.Cookies[i]);
                    }
                }

                foreach (var item in lstCookie.Where(p => p.Name.StartsWith("Cart_")))
                {
                    lstBasket.Add(new ViewModel.Home.BasketViewModel
                    {
                        Product = _blProduct.Find(Convert.ToInt32(item.Name.Substring(5))),
                        Count = Convert.ToInt32(item.Value)
                    });
                }

                var sumPrice = lstBasket.Sum(item => (item.Count * item.Product.Price));

                factor.BuyDate = DateTime.Now;
                factor.Price = sumPrice;
                factor.Description = "خرید توسط کاربر " + factor.Name + "در تاریخ" + DateTime.Now.ToPersianDateTime() +
                                     "به مبغ " + factor.Price.ToPrice() + "تومان انجام شد";
                //login
                if (Session["UserId"] != null)
                {
                    factor.UserId = Convert.ToInt32(Session["UserId"]);
                }
                factor.IsFinish = false;

                //then add

                if (_blFactor.Add(factor))
                {
                    var factorId = _blFactor.GetLastIdentity();
                    foreach (var item in lstBasket)
                    {
                        _blFactorItem.Add(new FactorItem()
                                            {
                                                FactorId = factorId,
                                                ProductId = item.Product.Id,
                                                Qty = Convert.ToByte(item.Count)
                                            });
                    }

                    //Redirect to pay...
                    //chon dar ajax emkan redirect nist pas nemitavanim qesmat else ro ba ajax por konim
                    //dar ajax faqt script,meqsar ya json va ... bargardanim 

                    var zarinPal = new ZarinPal.PaymentGatewayImplementationServicePortTypeClient();
                    if (Request.Url != null)
                    {
                        var result = "";
                        //?FactorId=" + factorId.ToString().Encrypt().UrlEncode() ba in kar mitonim id tarafo pas bedim be Verify
                        var code = zarinPal.PaymentRequest("MerchantId",
                                                  Convert.ToInt32(factor.Price),
                                                  factor.Description,
                                                  factor.Email,
                                                  factor.Mobile,
                                                  "Http://" + Request.Url.Authority + "/Home/Verify?factorId=" + factorId.ToString().Encrypt().UrlEncode(),
                                                  out result);
                        //if PaymentRequest is ok code==100
                        if (code == 100)
                        {
                            return Redirect("https://www.zarinpal.com/pg/StartPay/" + result);
                        }
                        else
                        {
                            ViewBag.Message1 = "در اتصال به درگاه بانکی خطا رخ داد";
                        }
                    }
                }
                else
                {
                    ViewBag.Message2 = "در ثبت اطلاعات خطا رخ داد";
                }
            }
            else
            {
                ViewBag.Message3 = "اطاعات خود را به درستی وارد کنید";
            }
            return View();
        }


        public ActionResult Verify(string factorId, string status, string authority)
        {
            if (string.IsNullOrEmpty(status) == false && string.IsNullOrEmpty(authority) == false && string.IsNullOrEmpty(factorId) == false && status.ToLower() == "ok")
            {
                var factor = _blFactor.Find(Convert.ToInt32(factorId.Decrypt()));
                long refId = 0;
                System.Net.ServicePointManager.Expect100Continue = false;
                var zarinPal = new ZarinPal.PaymentGatewayImplementationServicePortTypeClient();
                var statusZarinPal = zarinPal.PaymentVerification("MerchantId", authority, Convert.ToInt32(factor.Price), out refId);

                switch (statusZarinPal)
                {
                    case -1:
                        ViewBag.Message = "اطلاعات ارسال شده ناقص است.";
                        break;
                    case -11:
                        ViewBag.Message = "درخواست مورد نظر یافت نشد.";
                        break;
                    case -22:
                        ViewBag.Message = "تراکنش ناموفق می باشد.";
                        break;
                    case -33:
                        ViewBag.Message = "مبلغ تراکنش با مبلغ پرداخت شده مطابقت ندارد.";
                        break;
                    case 100:
                    case 101:
                        //Success 100 or 101 
                        factor.IsFinish = true;
                        factor.FollowCode = refId.ToString();

                        _blFactor.Update(factor);

                        ViewBag.Message2 = "تراکنش با موفقیت انجام شد. کد رهگیری : " + refId;

                        break;
                }
            }
            else
            {
                ViewBag.Message = "مقدار ورودی نا معتبر است";
            }
            return View();
        }

        public ActionResult ShowGroups(string groupName)
        {
            var model = new ViewModel.Home.ShowGroupViewModel();
            if (string.IsNullOrEmpty(groupName))
            {
                model.Groups = _blGroup.Where(p => p.ParentId == null);
                model.Products = _blProduct.Select();
                return View(model);
            }
            else
            {
                var src = _blGroup.Where(p => p.Name.Trim() == groupName.Trim());
                if (src.Any())
                {
                    model.Groups = src.Single().Groups.ToList();
                    var groupId = src.Single().Id;
                    model.Products = _blProduct.Where(p => p.GroupId == groupId);
                    return View(model);
                }
            }
            model.Groups = _blGroup.Where(p => p.ParentId == null);
            model.Products = _blProduct.Select();
            return View(model);

        }

        [ChildActionOnly]
        [OutputCache(Duration = 86400)]
        public PartialViewResult Menu()
        {
            //var model = new ViewModel.Home.ShowGroupViewModel();
            //model.Groups = _blGroup.Select ().ToList ();
           
            var model = _blGroup.Select ().ToList ();
            return PartialView("_Menu", model);
        }

        public ActionResult ShowProducts(string productName)
        {

            if (string.IsNullOrEmpty(productName))
            {
                return RedirectToAction("Index");
            }
            var src = _blProduct.Where(p => p.Url.Trim() == productName.Trim());
            if (src.Any())
            {
                return View(src.FirstOrDefault());
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        /////////////////////////////////////////////////////


        public ActionResult GetCities(int stateId)
        {
            var model = new ViewModel.Home.CityResellerViewModel();
            model.Cities = _blCity.Where(p => p.StateId == stateId).ToList();
            return PartialView("_CityReseller", model);
        }

        //with Mvcpager
        [HttpGet]
        public ActionResult ListReseller(int page = 1)
        {
            var reseller = _blReseller.Select();
            var totalItemCount = reseller.Count();
            var model = new ViewModel.Home.PageListViewModel
                        {
                            CurrentPage = page,
                            Resellers = reseller.OrderBy(p => p.Id).Skip((page - 1) * 10).Take(10).ToList(),
                            TotalItemCount = totalItemCount
                        };

            return View(model);
        }


        [HttpPost]
        public ActionResult Search(string txtSearch)
        {
            System.Threading.Thread.Sleep(2000);
            var model = new ViewModel.Home.PageListViewModel();
            model.Resellers = _blReseller.Where(p => p.Name.Contains(txtSearch) || p.Address.Contains(txtSearch));
            return PartialView("_SearchReseller", model);
        }



        //with Ajax Pager

        public ActionResult AjaxListReseller(int page = 1)
        {
            var reseller = _blReseller.Select();
            var totalItemCount = reseller.Count();
            var model = new ViewModel.Home.PageListViewModel
            {
                CurrentPage = page,
                Resellers = reseller.OrderBy(p => p.Id).Skip((page - 1) * 10).Take(10).ToList(),
                TotalItemCount = totalItemCount
            };

            return View(model);
        }


        public ActionResult GetAjaxListReseller(int page = 1)
        {
            var reseller = _blReseller.Select();
            var totalItemCount = reseller.Count();
            var model = new ViewModel.Home.PageListViewModel
            {
                CurrentPage = page,
                Resellers = reseller.OrderBy(p => p.Id).Skip((page - 1) * 10).Take(10).ToList(),
                TotalItemCount = totalItemCount
            };

            return PartialView("_AjaxListReseller", model);
        }
    }
}