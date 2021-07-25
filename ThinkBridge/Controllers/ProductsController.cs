using Microsoft.AspNet.OData;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ThinkBridge.DataAccess;
using ThinkBridge.Models ;

namespace ThinkBridgeSolution.Controllers
{
    public class ProductsController : ODataController
    {

        ProductDbContext db = new ProductDbContext();
        //Enable Query attribute enables client to modify the query by using query options such as $filter,$sort,$orderby etc.
        [EnableQuery]
        public IQueryable<Product> Get()
        {
            return db.Products;
        }

        [EnableQuery]
        public SingleResult<Product> Get([FromODataUri] int key)
        {
            IQueryable<Product> result = db.Products.Where(n => n.Id == key);
            return SingleResult.Create(result);
        }

        public async Task<IHttpActionResult> Post(Product product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            db.Products.Add(product);
            await db.SaveChangesAsync();
            return Created(product);
        }

        public async Task<IHttpActionResult> Put([FromODataUri]int key, Product product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (key != product.Id) return BadRequest();
            db.Entry(product).State = EntityState.Modified;
            try
            {
                int x = await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(key)) return NotFound();
            }
            return Updated(product);
        }


        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            var product = await db.Products.FindAsync(key);
            if (product == null) return NotFound();
            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
        private bool ProductExists(int key)
        {
            return db.Products.Any(n => n.Id == key);
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}

