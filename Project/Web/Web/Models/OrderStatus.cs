using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class OrderStatus
    {
        public int OrderStatusId { get; set; }

        public string Name { get; set; }
    }
}
