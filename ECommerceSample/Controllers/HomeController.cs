using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Entity;
using ECommerce.Repository;

namespace ECommerceSample.Controllers
{
    public class HomeController : Controller
    {

        ProductRepository pr = new ProductRepository();
        // GET: Home
        public ActionResult Index()
        {
            return View(pr.GetLastersObj(6).ProcessResult);
        }

        public ActionResult Detail(int id)
        {
            Product p = pr.GetObjById(id).ProcessResult;
            return View(p);
        }

        public ActionResult List(Guid? id)
        {
            List<Product> pList = pr.List().ProcessResult.Where(t => t.CategoryId == id).ToList();
            return View(pList);
        }

        public ActionResult ListByBrand(int? id)
        {
            List<Product> pList = pr.List().ProcessResult.Where(t => t.BrandId == id).ToList();
            return View(pList);
        }

        public ActionResult ListAllProduct()
        {
            return View(pr.List().ProcessResult);
        }
    }
}