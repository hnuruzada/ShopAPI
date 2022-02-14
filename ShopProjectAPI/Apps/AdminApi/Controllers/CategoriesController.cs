using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopProjectAPI.Apps.AdminApi.DTOs;
using ShopProjectAPI.Apps.AdminApi.DTOs.CategoryDtos;
using ShopProjectAPI.Data.DAL;
using ShopProjectAPI.Data.Entity;
using ShopProjectAPI.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopProjectAPI.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("admin/api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ShopDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public CategoriesController(ShopDbContext context,IWebHostEnvironment env, IMapper mapper)
        {
            _context = context;
            _env=env;
            _mapper=mapper;
        }

        [HttpPost("")]
        public IActionResult Create([FromForm] CategoryPostDto catregoryDto)
        {
            if (_context.Categories.Any(x => x.Name.ToUpper() == catregoryDto.Name.Trim().ToUpper()))
                return StatusCode(409);
           
            
            Category category = new Category
            {
                Name = catregoryDto.Name,
            };

            category.Image = catregoryDto.Image.SaveImg(_env.WebRootPath, "Assets/CategoryImg");
            _context.Categories.Add(category);
            _context.SaveChanges();


            return StatusCode(201, category);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Category category = _context.Categories.FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (category == null) return NotFound();

            CategoryGetDto categoryDto = _mapper.Map<CategoryGetDto>(category);

            return Ok(categoryDto);
        }

        [HttpGet("")]
        public IActionResult GetAll(int page = 1)
        {
            var query = _context.Categories.Where(x => !x.IsDeleted);

            ListDto<CategoryListItemDto> listDto = new ListDto<CategoryListItemDto>
            {
                TotalCount = query.Count(),
                Items = query.Skip((page - 1) * 8).Take(8).Select(x => new CategoryListItemDto { Id = x.Id, Name = x.Name,Image=x.Image }).ToList()
            };

            return Ok(listDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,[FromForm] CategoryPostDto categoryDto)
        {
            Category category = _context.Categories.FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (category == null) return NotFound();

            if (categoryDto.Image != null)
            {
                

                Helpers.Helper.DeleteImg(_env.WebRootPath, "Assets/CategoryImg", category.Image);
                category.Image = categoryDto.Image.SaveImg(_env.WebRootPath, "Assets/CategoryImg");
            }

            if (_context.Categories.Any(x => x.Id != id && x.Name.ToUpper() == categoryDto.Name.Trim().ToUpper()))
                return StatusCode(409);


            category.Name = categoryDto.Name;
            category.ModifiedAt = DateTime.UtcNow;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Category category = _context.Categories.FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (category == null) return NotFound();
            Helpers.Helper.DeleteImg(_env.WebRootPath, "Assets/CategoryImg", category.Image);
            category.IsDeleted = true;
            category.ModifiedAt = DateTime.UtcNow;
            _context.SaveChanges();

            return NoContent();
        }
    }

}
