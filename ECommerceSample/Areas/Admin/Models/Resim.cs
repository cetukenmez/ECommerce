using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerceSample.Areas.Admin.Models
{
    public class Resim
    {
        public IEnumerable<HttpPostedFileBase> photo { get; set; }
    }
}