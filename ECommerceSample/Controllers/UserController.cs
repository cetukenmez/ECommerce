using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Entity;
using ECommerce.Repository;
using ECommerce.Common;
using ECommerceSample.Areas.Admin.Models.ResultModel;
using ECommerceSample.Areas.Admin.Models.ViewModel;

namespace ECommerceSample.Controllers
{
    public class UserController : Controller
    {
        InstanceResult<Member> rrr = new InstanceResult<Member>();
        InstanceResult<Order> result = new InstanceResult<Order>();
        MemberRepository mr = new MemberRepository();
        OrderRepository or = new OrderRepository();
        // GET: User
        public ActionResult Profilim()
        {
            int id = int.Parse(User.Identity.Name);
            rrr.resultList = mr.List();
            return View(rrr.resultList.ProcessResult.Where(t => t.UserId == id));
        }
        [Authorize]
        [HttpGet]
        public ActionResult ProfilDuzenle(int id)
        {
            rrr.Tresult = mr.GetObjById(id);
            return View(rrr.Tresult.ProcessResult);
        }

        [Authorize]
        [HttpPost]
        public ActionResult ProfilDuzenle(Member model)
        {
            rrr.resultint = mr.Update(model);
            if (rrr.resultint.IsSucceeded)
            {
                return RedirectToAction("Profilim");
            }
            return View(model);
        }

        public ActionResult Siparislerim()
        {
            int id = int.Parse(User.Identity.Name);
            result.resultList = or.List();
            return View(result.resultList.ProcessResult.Where(t => t.MemberId == id).OrderByDescending(t => t.OrderDate));
        }
    }
}