﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Common;
using ECommerce.Entity;

namespace ECommerce.Repository
{
    public class OrderRepository : DataRepository<Order, int>
    {
        private static MyECommerceEntities db = Tools.GetConnection();
        ResultProcess<Order> result = new ResultProcess<Order>();
        public override Result<int> Delete(int id)
        {
            Order delete = db.Orders.SingleOrDefault(t => t.OrderId == id);
            db.Orders.Remove(delete);
            return result.GetResult(db);
        }

        public override Result<List<Order>> GetLastersObj(int Quantity)
        {
            return result.GetListResult(db.Orders.OrderByDescending(t => t.OrderId).Take(Quantity).ToList());
        }

        public override Result<Order> GetObjById(int id)
        {
            Order obj = db.Orders.SingleOrDefault(t => t.OrderId == id);
            return result.GetT(obj);
        }

        public override Result<int> Insert(Order item)
        {
            db.Orders.Add(item);
            return result.GetResult(db);
        }

        public override Result<List<Order>> List()
        {
            List<Order> OrdList = db.Orders.ToList();
            return result.GetListResult(OrdList);
        }

        public override Result<int> Update(Order item)
        {
            Order updt = db.Orders.SingleOrDefault(t => t.OrderId == item.OrderId);
            updt.IsPay = item.IsPay;
            updt.TotalPrice = item.TotalPrice;
            return result.GetResult(db);
        }
    }
}
