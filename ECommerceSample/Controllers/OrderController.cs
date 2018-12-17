using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Entity;
using ECommerce.Repository;
using ECommerceSample.Areas.Admin.Models.ResultModel;
using ECommerce.Common;
using ECommerceSample.Areas.Admin.Models.ViewModel;


namespace ECommerceSample.Controllers
{
    public class OrderController : Controller
    {
        OrderRepository or = new OrderRepository();
        ProductRepository pr = new ProductRepository();
        OrderDetailRep ordrep = new OrderDetailRep();
        InstanceResult<Product> rp = new InstanceResult<Product>();
       
        InstanceResult<OrderDetail> result = new InstanceResult<OrderDetail>();
        // GET: Order
        public ActionResult Add(int id , ProductViewModel model)
        {
            //sepetimizi session da tutuyoruz buradaki sessionun adı order(session)[order])
            if (Session["Order"] == null)
            {
                Order o = new Order();
                o.OrderDate = DateTime.Now;
                o.MemberId = int.Parse(User.Identity.Name);
                o.IsPay = false;
                or.Insert(o);
                Session["Order"] = or.GetLastersObj(1).ProcessResult[0];
                OrderDetail od = new OrderDetail();
                od.OrderId = ((Order)Session["Order"]).OrderId;
                od.ProductId = id;
                Product stok = pr.GetObjById(od.ProductId).ProcessResult;
                stok.Stock--;
                pr.Update(stok);
                od.Quantity = 1;               
                od.Price = pr.GetObjById(id).ProcessResult.Price;

                    //od.Product.Stock -= pr.GetObjById(id).ProcessResult.Stock;
                    //rp.resultint = pr.Update(model.Product);

                ordrep.Insert(od);                
            }
            else
            {
                Order o = (Order)Session["Order"];
                OrderDetail update = ordrep.GetOrderDetByTwoID(o.OrderId, id).ProcessResult;
                if (update==null)
                {
                    OrderDetail od = new OrderDetail();
                    od.OrderId = o.OrderId;
                    od.ProductId = id;
                    od.Quantity = 1;
                    Product stok = pr.GetObjById(od.ProductId).ProcessResult;
                    stok.Stock--;
                    pr.Update(stok);
                    od.Price = pr.GetObjById(id).ProcessResult.Price;
                    ordrep.Insert(od);
                }
                else
                {
                    Product stok = pr.GetObjById(update.ProductId).ProcessResult;
                    stok.Stock--;
                    pr.Update(stok);
                    update.Quantity++;
                    update.Price += pr.GetObjById(id).ProcessResult.Price;
                    ordrep.Update(update);
                }
            }
            return RedirectToAction("Index","Home");
        }

        public ActionResult DetailList()          
        {
            decimal? total = 0;
            Order sepetim = (Order)Session["Order"];
            if (sepetim.OrderDetails != null)
            {
                foreach (OrderDetail item in sepetim.OrderDetails)
                {
                    total += item.Price;
                }
                sepetim.TotalPrice = total;
                or.Update(sepetim);
            }
            else
            {
                sepetim.TotalPrice = 0;
                or.Update(sepetim);

            }
            if (sepetim == null)
            {
                return RedirectToAction("ListAllProduct", "Home");
            }
            else
            {
                return View(sepetim.OrderDetails);
            }

        }

        public ActionResult Delete(int id)
        {
            Order  idd = (Order)Session["Order"];
            Result<int> result = ordrep.OrderDetailSil(idd.OrderId,id);
            return RedirectToAction("DetailList");
        }
    }
}