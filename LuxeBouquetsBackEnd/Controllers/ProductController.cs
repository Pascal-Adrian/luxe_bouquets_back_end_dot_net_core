using LuxeBouquetsBackEnd.Models;
using LuxeBouquetsBackEnd.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LuxeBouquetsBackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly DataBaseContext dbContext;

        public ProductController(DataBaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(dbContext.Products.ToList());
        }

        [HttpGet]
        [Route("id={id:int}")]
        public IActionResult GetProductById(int id)
        {
            var product = dbContext.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductDto productDto)
        {
            var category = dbContext.Categories.Find(productDto.CategoryId);

            if (category == null)
            {
                return BadRequest("Category not found");
            }

            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                ImageUrl = productDto.ImageUrl,
                Category = category,
                SubCategory = productDto.SubCategory,
                Description = productDto.Description
            };

            dbContext.Products.Add(product);
            dbContext.SaveChanges();

            return Ok(product);
        }

        [HttpPut]
        [Route("id={id:int}")]
        public IActionResult UpdateProduct(int id, ProductDto productDto)
        {
            var product = dbContext.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            var category = dbContext.Categories.Find(productDto.CategoryId);

            if (category == null)
            {
                return BadRequest("Category not found");
            }

            product.Name = productDto.Name;
            product.Price = productDto.Price;
            product.ImageUrl = productDto.ImageUrl;
            product.Category = category;
            product.SubCategory = productDto.SubCategory;
            product.Description = productDto.Description;

            dbContext.SaveChanges();

            return Ok(product);
        }

        [HttpDelete]
        [Route("id={id:int}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = dbContext.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            dbContext.Products.Remove(product);
            dbContext.SaveChanges();

            return Ok();
        }
    }
}