using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XuongMay.ModelViews.OrderModelView
{
    public class OrderModelView
    {
        public string OrderID { get; set; } = string.Empty;
        public string ProductID { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
