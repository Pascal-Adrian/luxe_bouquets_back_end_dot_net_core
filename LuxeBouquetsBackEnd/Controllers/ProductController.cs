using LuxeBouquetsBackEnd.Models;
using LuxeBouquetsBackEnd.Models.Entities;
using LuxeBouquetsBackEnd.Services;
using Microsoft.AspNetCore.Mvc;

namespace LuxeBouquetsBackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly DataBaseContext dbContext;
        private readonly UrlConvertService urlConvertService;

        public ProductController(DataBaseContext dbContext)
        {
            this.dbContext = dbContext;
            this.urlConvertService = new UrlConvertService();
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
            if (!urlConvertService.IsGoogleDriveUrl(productDto.ImageUrl))
            {
                return BadRequest("Invalid image url, only Google Drive urls are allowed.");
            }

            string imageUrl = urlConvertService.ConvertGoogleDriveUrl(productDto.ImageUrl);

            var category = dbContext.Categories.Find(productDto.CategoryId);

            if (category == null)
            {
                return BadRequest("Category not found");
            }

            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                ImageUrl = imageUrl,
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

            if (!urlConvertService.IsGoogleDriveUrl(productDto.ImageUrl))
            {
                return BadRequest("Invalid image url, only Google Drive urls are allowed.");
            }

            var imageUrl = urlConvertService.ConvertGoogleDriveUrl(productDto.ImageUrl);

            var category = dbContext.Categories.Find(productDto.CategoryId);

            if (category == null)
            {
                return BadRequest("Category not found");
            }

            product.Name = productDto.Name;
            product.Price = productDto.Price;
            product.ImageUrl = imageUrl;
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