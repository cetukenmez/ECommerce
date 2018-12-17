using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Common;
using ECommerce.Entity;
using ECommerce.Repository;
using ECommerceSample.Areas.Admin.Models.ResultModel;

namespace ECommerceSample.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {

        CategoryRepository cr = new CategoryRepository();
        InstanceResult<Category> result = new InstanceResult<Category>();
        //Result<List<Category>> result = new Result<List<Category>>();
        //Result<int> resultint = new Result<int>();
        //Result<Category> resultt = new Result<Category>();

        // GET: Admin/Category
        public ActionResult List(string mesaj)
        {
            result.resultList=cr.List();
            ViewBag.Mesaj = mesaj;
            return View(result.resultList.ProcessResult);
        }

        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category model)
        {
            model.CategoryId = Guid.NewGuid();
            result.resultint = cr.Insert(model);
            ViewBag.Mesaj = result.resultint.UserMessage;
            return View();
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            result.Tresult = cr.GetObjById(id);
            return View(result.Tresult.ProcessResult);
        }
        [HttpPost]
        public ActionResult Edit(Category model)
        {
            result.resultint = cr.Update(model);
            ViewBag.Mesaj = result.resultint.UserMessage;
            return View();
        }
        public ActionResult Delete(Guid id)
        {
            result.resultint = cr.Delete(id);
            return RedirectToAction("List", new { @mesaj = result.resultint.UserMessage });
        }
    }
}