using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopProjectAPI.Apps.AdminApi.DTOs;
using ShopProjectAPI.Apps.AdminApi.DTOs.ProductDtos;
using ShopProjectAPI.Data.DAL;
using ShopProjectAPI.Data.Entity;
using System.Linq;

namespace ShopProjectAPI.Controllers
{
    [Route("admin/api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ShopDbContext _context;
        private readonly IMapper _mapper;
        public ProductsController(ShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

       
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Product product = _context.Products.Include(x=>x.Category).ThenInclude(c=>c.Products).FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (product == null) return NotFound();

           

            ProductGetDto productDto = _mapper.Map<ProductGetDto>(product);



            return Ok(productDto);
        }

        [Route("")]
        [HttpGet]
        public IActionResult GetAll(int page = 1, string search = null)
        {
            var query = _context.Products.Include(x=>x.Category).Where(x => !x.IsDeleted);

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(x => x.Name.Contains(search));

            ListDto<ProductListItemDto> listDto = new ListDto<ProductListItemDto>
            {
                Items = query.Skip((page - 1) * 8).Take(8).Select(x =>
                    new ProductListItemDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        SalePrice = x.SalePrice,
                        CostPrice = x.CostPrice,
                         Category = new CategoryInProductListItemDto
                         {

                             Id = x.CategoryId,
                             Name = x.Category.Name
                            
                            
                         }

                    }).ToList(),
                TotalCount = query.Count()
            };

           


            return Ok(listDto);
        }

        [Route("")]
        [HttpPost]
        public IActionResult Create(ProductPostDto productDto)
        {
            //Product product = new Product
            //{
            //    Name = productDto.Name,
            //    SalePrice = productDto.SalePrice,
            //    CostPrice = productDto.CostPrice,
            //    CategoryId = productDto.CategoryId

            //};
            Product product = _mapper.Map<Product>(productDto);

            _context.Products.Add(product);
            _context.SaveChanges();

            return StatusCode(201, product);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ProductPostDto productDto)
        {
            Product existProduct = _context.Products.FirstOrDefault(x => x.Id == id);

            if (existProduct == null)
                return NotFound();

            if(existProduct.CategoryId != productDto.CategoryId && !_context.Categories.Any(c => c.Id == productDto.CategoryId && !c.IsDeleted))
                return NotFound();
            existProduct.CategoryId = productDto.CategoryId;
            existProduct.Name = productDto.Name;
            existProduct.SalePrice = productDto.SalePrice;
            existProduct.CostPrice = productDto.CostPrice;
            

            _context.SaveChanges();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Product product = _context.Products.FirstOrDefault(x => x.Id == id);

            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();


            return NoContent();
        }

        
    }
}
