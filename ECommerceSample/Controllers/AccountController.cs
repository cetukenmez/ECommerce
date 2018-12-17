using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerceSample.Models.VM;
using ECommerce.Entity;
using ECommerce.Repository;
using System.Web.Security;
using ECommerceSample.Areas.Admin.Models.ResultModel;
using ECommerceSample.Areas.Admin.Models.ViewModel;

namespace ECommerceSample.Controllers
{
    public class AccountController : Controller
    {
        MemberRepository mr = new MemberRepository();
        InstanceResult<Member> result = new InstanceResult<Member>();
        
        // GET: Account
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {

                using ( MyECommerceEntities context = new MyECommerceEntities())
                {
                    var user = context.Members.FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);
                    if (user != null)
                    {
                        //FormsAuthentication.SetAuthCookie(user.UserName, true);
                        FormsAuthentication.SetAuthCookie(user.UserId.ToString(), true);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            ViewBag.Message = "Kullanici adi veya Parola Yanlış";
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Uye()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Uye(Member model)
        {
            model.RoleId = 3;
            result.resultint = mr.Insert(model);
            if (result.resultint.ProcessResult > 0)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

    }
}