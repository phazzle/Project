﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class OrderType
    {
        public int OrderTypeId { get; set; }


        public string Name { get; set; }
    }
}
