using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ThinkBridge.DataAccess
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext() : base("ProductsDatabase")
        {
            this.Database.Connection.ConnectionString = @"Data Source = .; Initial Catalog=ProductDb; Integrated Security=True;";
        }
        public DbSet<Models.Product> Products { get; set; }
    }
}