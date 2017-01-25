using Shop.Models.EntityModels;
using Shop.Models.Repositories;
using Shop.ViewModel.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Helpers.Filters;
using Shop.ViewModel.Admin;
using Shop.Helpers.Utilties;
using System.Web.UI;

namespace Shop.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        readonly GroupRepository _blGroup = new GroupRepository(new DatabaseContext());
        readonly ProductRepository _blProduct = new ProductRepository(new DatabaseContext());
        readonly FactorRepository _blFactor = new FactorRepository(new DatabaseContext());
        readonly ResellerRepository _blReseller = new ResellerRepository(new DatabaseContext());
        readonly StateRepository _blState = new StateRepository(new DatabaseContext());
        public ActionResult Groups()
        {

            var model = new AddGroupViewModel();
            model.Groups = _blGroup.Select().ToList();
            return View(model);
        }


        [HttpPost]
        [AjaxOnly]
        public ActionResult AddGroup(Group group)
        {
            if (ModelState.IsValid)
            {
                if (_blGroup.Add(group))
                {
                    return Json(new JsonData()
                                  {
                                      Script = MessageBox.Show("اطلاعات با موفقیت ثبت شد",MessageType.Success).Script,
                                      Success = true,
                                      Html = this.RenderPartialToString("_GroupList",_blGroup.Select().ToList())

                                  });
                }
                else
                {
                    return Json(new JsonData()
                    {
                        Script = MessageBox.Show("در ثبت اطلاعات خطا رخ داد !", MessageType.Error).Script,
                        Success = false,
                        Html = ""
                    });
                }

            }
            else
            {
                // when client validation disbale just active sever validation
                return Json(new JsonData()
                {
                    Script = MessageBox.Show(ModelState.GetErrors(), MessageType.Warning).Script,
                    Success = false,
                    Html = ""
                });

            }

        }

       

        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteGroup(int id)
        {
            if (_blGroup.Delete(id))
            {
                return Json(new JsonData()
                {
                    Script = MessageBox.Show("اطلاعات با موفقیت حذف شد", MessageType.Success).Script,
                    Success = true,
                    Html = this.RenderPartialToString("_GroupList", _blGroup.Select().ToList())
                });
            }
            else
            {
                return Json(new JsonData()
                {
                    Script = MessageBox.Show("در حذف اطلاعات خطا رخ داد ", MessageType.Error).Script,
                    Success = false,
                    Html = ""
                });
            }
        }


        [HttpPost]
        [AjaxOnly]
        public ActionResult EditGroup(Group group)
        {

            if (ModelState.IsValid && group.Id != 0)
            {
                if (_blGroup.Update(group))
                {
                    return Json(new JsonData()
                    {
                        Script = MessageBox.Show("اطلاعات با موفقیت ویرایش شد",
                                                   MessageType.Success).Script,
                        Success = true,
                        Html = this.RenderPartialToString("_GroupList",
                                                            _blGroup.Select().ToList())

                    });
                }
                else
                {
                    return Json(new JsonData()
                    {
                        Script = MessageBox.Show("در ویرایش اطلاعات خطا رخ داد !", MessageType.Error).Script,
                        Success = false,
                        Html = ""
                    });
                }

            }
            else
            {
                // when client validation disbale just active sever validation
                return Json(new JsonData()
                {
                    Script = MessageBox.Show(ModelState.GetErrors(), MessageType.Warning).Script,
                    Success = false,
                    Html = ""
                });

            }

        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            var model = new AddProductViewModel();
            model.Groups = _blGroup.Select ();
            return View(model);
        }

        [AjaxOnly]
        [HttpPost]

        public ActionResult AddProduct(Product product,HttpPostedFileBase uploadImage )
        {
            try
            {
                if ( uploadImage!=null )
                {
                    product.Image = uploadImage.FileName;
                    var path = Server.MapPath("~") + "Files\\UploadImages\\" + uploadImage.FileName;
                    uploadImage.InputStream.ResizeImageByWidth(500, path, Utilty.ImageComperssion.Normal);
                   
              
                    if (ModelState.IsValid)
                    {
                        if (_blProduct.Add(product))
                        {
                            return MessageBox.Show("اطلاعات با موفقیت ثبت شد", MessageType.Success);
                        }
                        else
                        {
                            System.IO.File.Delete(path);
                            return MessageBox.Show("در ثبت اطلاعات خطا رخ داد !", MessageType.Error);
                        }
                    }
                    else
                    {
                        return MessageBox.Show(ModelState.GetErrors(), MessageType.Warning);
                    }
                }

                if (ModelState.IsValid)
                {
                    if (_blProduct.Add(product))
                    {
                        return MessageBox.Show("اطلاعات با موفقیت ثبت شد", MessageType.Success);
                    }
                    else
                    {
                        //System.IO.File.Delete(path);
                        return MessageBox.Show("در ثبت اطلاعات خطا رخ داد !", MessageType.Error);
                    }
                }
                return MessageBox.Show(ModelState.GetErrors(), MessageType.Warning);
               
            }
            catch ( Exception )
            {
                return MessageBox.Show(ModelState.GetErrors(), MessageType.Warning);
            }

        }

        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult CheckUrl(string url)
            {
            //url = "12";
            var colection = _blProduct.Select(p => p.Url);
            foreach (var item in colection)
            {
                if (item == url)
                {
                    return Json(false);
                }
            }
            return Json(true);
        }
        [HttpGet]
        public ActionResult Products ()
        {
            var model = _blProduct.Select ();
            return View (model);
        }

       
 
        public ActionResult DeleteProduct(int id)
        {
            _blProduct.Delete ( id );
            return RedirectToAction( "Products");
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult EditProduct ( Product product ,HttpPostedFileBase uploadImage )
        {
           if ( ModelState.IsValid )
            {
                string path = null;
                if ( uploadImage!=null)
                {
                    path = uploadImage.FileName;
                    uploadImage.InputStream.ResizeImageByWidth ( 500 ,
                                                                 Server.MapPath ( "~" ) + "Files\\UploadImages\\" + path ,
                                                                 Utilty.ImageComperssion.Normal );
                }
                if ( _blProduct.Update ( product ,path ) )
                {
                    return MessageBox.Show ( "اطلاعات با موفقیت ویرایش شد" ,MessageType.Success );
                }
                else
                {
                    if ( path != null )
                    {
                        System.IO.File.Delete ( Server.MapPath ( "~" ) + "Files\\UploadImages\\" + path );
                       
                    }
                    return MessageBox.Show("در ثبت اطلاعات خطا رخ داد !",MessageType.Error);
                }
            }
            else
            {
                return MessageBox.Show ( ModelState.GetErrors () ,MessageType.Warning );
            }
        }
        
        [HttpGet]
        public ActionResult EditProduct(int id )
        {
            var model = new AddProductViewModel();
            model.Groups = _blGroup.Select ();
            model.Product = _blProduct.Find ( id );
            return View ( model );

        }

        [HttpGet]
        public ActionResult Factors()
        {
            var model = _blFactor.Select ().OrderByDescending( p => p.IsFinish ).ToList();
           return View(model);
        }

        [HttpGet]
        public ActionResult SendMail(string email)
        {
            return View(model:email);
        }

    

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SendMail(string recivers, string title, string text, HttpPostedFileBase attachment)
        {
            var result = false;
            if (attachment!=null)
            {
                var path =Server.MapPath ( "~" + "\\Files\\Attachment\\" +
                                         System.IO.Path.GetFileName ( attachment.FileName ) );
                attachment.SaveAs(path);
               result= MailSender.SendMailByAttach( title ,text ,path,recivers.Split ( ',' ) );
            }
            else
            {
              result=  MailSender.SendMail ( title ,text ,recivers.Split ( ',' ) );
            }
            if ( result )
             {
                return MessageBox.Show("پیام با موفقیت ارسال شد", MessageType.Success);
            }
            return MessageBox.Show("در ارسال پیام خطا رخ داد", MessageType.Error);
        }


        [Authorize]
        [HttpGet]
        public ActionResult AddReseller()
        {
            var model = new ViewModel.Admin.AddResellerViewModel();
            model.States = _blState.Select();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddReseller(Reseller reseller)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_blReseller.Add(reseller))
                    {
                        //ViewBag.Message = "نمایندگی با موفقیت ثبت شد";
                        return MessageBox.Show("نمایندگی با موفقیت ثبت شد", MessageType.Success);
                    }
                    else
                    {
                        // ViewBag.Message = "نمایندگی ثبت نشد";
                        return MessageBox.Show("نمایندگی ثبت نشد", MessageType.Error);
                    }

                }
                else
                {
                    // ViewBag.Message = "مقادیر ورودی نا معتبر است";
                    return MessageBox.Show(ModelState.GetErrors(), MessageType.Error);
                }
            }
            catch (Exception ex)
            {

                return MessageBox.Show(ex.ToString(), MessageType.Alert);
            }

            ////age be sorat html beferestim baraye inke bad az submit form khali nabashe modelo pas midim
            //var model = new ViewModel.Home.AddResellerViewModel();
            //model.States = _blState.Select().ToList();
            //model.Reseller = reseller;
            //return View(model);
        }
    }
}