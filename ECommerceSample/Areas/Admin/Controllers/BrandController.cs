using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Entity;
using ECommerce.Common;
using ECommerce.Repository;
using ECommerceSample.Areas.Admin.Models.ResultModel;

namespace ECommerceSample.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        // GET: Admin/Brand
        BrandRepository br = new BrandRepository();
        InstanceResult<Brand> result = new InstanceResult<Brand>();
        //Result<List<Brand>> resultList = new Result<List<Brand>>();
        //Result<int> resultint = new Result<int>();
        //Result<Brand> resultt = new Result<Brand>();
        public ActionResult List()
        {
            result.resultList = br.List();
            return View(result.resultList.ProcessResult);
        }

        
        public ActionResult AddBrand()
        {
            Brand b = new Brand();
            b.Photo = "denemetestdeneme";
            return View(b);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddBrand(Brand model,HttpPostedFileBase photoPath)
        {
            string photoName = "";
            if (photoPath.ContentLength >0 & photoPath!=null)
            {
                photoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                string path = Server.MapPath("~/Upload/" + photoName);
                photoPath.SaveAs(path);
            }
            model.Photo = photoName;
            if (ModelState.IsValid)
            {
                result.resultint = br.Insert(model);
                if (result.resultint.IsSucceeded)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    ViewBag.Mesaj = result.resultint.UserMessage;
                    return View(model);
                }
            }
            else
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            result.Tresult = br.GetObjById(id);
            return View(result.Tresult.ProcessResult);
        }
        [HttpPost]
        public ActionResult Edit(Brand model , HttpPostedFileBase photoPath)
        {
            string PhotoName = model.Photo;
            if (photoPath.ContentLength >0 & photoPath !=null)
            {
                PhotoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                string path = Server.MapPath("~/Upload/" + PhotoName);
                photoPath.SaveAs(path);
            }
            model.Photo = PhotoName;
            result.resultint = br.Update(model);
            if (result.resultint.IsSucceeded)
            {
                return RedirectToAction("List");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            result.resultint = br.Delete(id);
            return RedirectToAction("List", new { @mesaj = result.resultint.UserMessage });
        }


    }
}