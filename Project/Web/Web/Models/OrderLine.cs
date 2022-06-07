using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class OrderLine
    {
        public int OrderLineId { get; set; }
        public int LineNumber { get; set; }
        public string ProductCode { get; set; }
        public int ProductTypeId { get; set; }


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#.#}")]
        public string CostPrice { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#.#}")]
        public string SalesPrice { get; set; }
        public int Quantity { get; set; }

        public int OrderHeaderId { get; set; }

        public OrderHeader OrderHeader { get; set; }
        public ProductType ProductType { get; set; }
    }
}
