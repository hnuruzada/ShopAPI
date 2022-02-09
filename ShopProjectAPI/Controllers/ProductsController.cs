using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopProjectAPI.Data.DAL;
using ShopProjectAPI.Data.Entity;
using System.Linq;

namespace ShopProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly ShopDbContext _context;
        public ProductsController(ShopDbContext context)
        {
            _context = context;
        }
        //[Route("{id}")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Product product = _context.Products.Include(p=>p.Category).FirstOrDefault(x => x.Id == id);

            if (product == null) return NotFound();

            return Ok();

        }

        [Route("")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return StatusCode(200, _context.Products.ToList());
        }

        [Route("")]
        [HttpPost]
        public IActionResult Create(Product product)
        {
           
            _context.Products.Add(product);
            _context.SaveChanges();

            return StatusCode(201, product);
        }

        [HttpPut("")]
        public IActionResult Update(Product product)
        {
            Product existProduct = _context.Products.FirstOrDefault(x => x.Id == product.Id);

            if (existProduct == null)
                return NotFound();

            existProduct.Name = product.Name;
            existProduct.SalePrice = product.SalePrice;
            existProduct.CostPrice = product.CostPrice;
            existProduct.CategoryId= product.CategoryId;

            _context.SaveChanges();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Product product = _context.Products.Include(p=>p.Category).FirstOrDefault(x => x.Id == id);

            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();


            return NoContent();
        }

       
    }
}
