using LuxeBouquetsBackEnd.Models;
using LuxeBouquetsBackEnd.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LuxeBouquetsBackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly DataBaseContext dbContext;

        public CategoryController(DataBaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            return Ok(dbContext.Categories.ToList());
        }

        [HttpGet]
        [Route("id={id:int}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = dbContext.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        public IActionResult CreateCategory(CategoryDto categoryDto)
        {
            var category = new Category
            {
                Name = categoryDto.Name,
                ImageUrl = categoryDto.ImageUrl
            };

            dbContext.Categories.Add(category);
            dbContext.SaveChanges();

            return Ok(category);
        }

        [HttpPut]
        [Route("id={id:int}")]
        public IActionResult UpdateCategory(int id, CategoryDto categoryDto)
        {
            var category = dbContext.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            category.Name = categoryDto.Name;
            category.ImageUrl = categoryDto.ImageUrl;

            dbContext.SaveChanges();

            return Ok(category);
        }

        [HttpDelete]
        [Route("id={id:int}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = dbContext.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            dbContext.Categories.Remove(category);
            dbContext.SaveChanges();

            return Ok();
        }
    }
}