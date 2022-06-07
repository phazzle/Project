using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class OrderHeader
    {
        public int OrderHeaderId { get; set; }
        public int OrderNumber { get; set; }
        public int OrderTypeId { get; set; }
        public int OrderStatusId { get; set; }

        public string CustomerName { get; set; }

        public DateTime CreateDate { get; set; }

        public ICollection<OrderLine> OrderLines { get; set; }
        public OrderStatus Status { get; set; }
        public OrderType OrderType { get; set; }


    }
}
