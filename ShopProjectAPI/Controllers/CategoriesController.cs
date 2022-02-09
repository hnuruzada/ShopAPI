using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopProjectAPI.Data.DAL;
using ShopProjectAPI.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace ShopProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly ShopDbContext _context;
        public CategoriesController(ShopDbContext context)
        {
            _context = context;
        }
        //[Route("{id}")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Category category = _context.Categories.FirstOrDefault(x => x.Id == id);

            if (category == null) return NotFound();

            //return StatusCode(200, product);
            return Ok();
        }

        [Route("")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return StatusCode(200, _context.Categories.ToList());
        }

        [Route("")]
        [HttpPost]
        
        public IActionResult Create(Category category)
        {
           
          
            _context.Categories.Add(category);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("")]
        public IActionResult Update(Category category, int id)
        {


            if (!ModelState.IsValid) return StatusCode(404);
           
            Category existedCategory = _context.Categories.FirstOrDefault(c => c.Id == category.Id);
            if (existedCategory == null) return NotFound();
           

            
            existedCategory.Name = category.Name;
            _context.SaveChanges();
            return NoContent();

        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Category category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return StatusCode(404);

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return StatusCode(200);
        }


    }
}
