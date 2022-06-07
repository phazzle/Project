using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Data
{
    public class WebContext : DbContext
    {
        public WebContext (DbContextOptions<WebContext> options)
            : base(options)
        {
        }

        public DbSet<Web.Models.ProductType>? ProductType { get; set; }

        public DbSet<Web.Models.OrderHeader>? OrderHeader { get; set; }

        public DbSet<Web.Models.OrderLine>? OrderLine { get; set; }

        public DbSet<Web.Models.OrderStatus>? OrderStatus { get; set; }

        public DbSet<Web.Models.OrderType>? OrderType { get; set; }


    }
}
