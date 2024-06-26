using LuxeBouquetsBackEnd.Models;
using LuxeBouquetsBackEnd.Models.Entities;
using LuxeBouquetsBackEnd.Services;
using Microsoft.AspNetCore.Mvc;

namespace LuxeBouquetsBackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionPlanController : ControllerBase
    {
        private readonly DataBaseContext dbContext;
        private readonly UrlConvertService urlConvertService;

        public SubscriptionPlanController(DataBaseContext dbContext)
        {
            this.dbContext = dbContext;
            this.urlConvertService = new UrlConvertService();
        }

        [HttpGet]
        public IActionResult GetSubscriptionPlans()
        {
            return Ok(dbContext.SubscriptionPlans.ToList());
        }

        [HttpGet]
        [Route("id={id:int}")]
        public IActionResult GetSubscriptionPlanById(int id)
        {
            var subscriptionPlan = dbContext.SubscriptionPlans.Find(id);

            if (subscriptionPlan == null)
            {
                return NotFound();
            }

            return Ok(subscriptionPlan);
        }

        [HttpPost]
        public IActionResult CreateSubscriptionPlan(SubscribtionPlanDto subscribtionPlanDto)
        {
            if (!urlConvertService.IsGoogleDriveUrl(subscribtionPlanDto.ImageUrl))
            {
                return BadRequest("Invalid image url, only Google Drive urls are allowed.");
            }

            string imageUrl = urlConvertService.ConvertGoogleDriveUrl(subscribtionPlanDto.ImageUrl);

            var subscriptionPlan = new SubscriptionPlan
            {
                Name = subscribtionPlanDto.Name,
                Price = subscribtionPlanDto.Price,
                FreeDelivery = subscribtionPlanDto.FreeDelivery,
                Details = subscribtionPlanDto.Details,
                Savings = subscribtionPlanDto.Savings,
                ImageUrl = imageUrl,
            };

            dbContext.SubscriptionPlans.Add(subscriptionPlan);
            dbContext.SaveChanges();

            return Ok(subscriptionPlan);
        }

        [HttpPut]
        [Route("id={id:int}")]
        public IActionResult UpdateSubscriptionPlan(int id, SubscribtionPlanDto subscribtionPlanDto)
        {
            if (!urlConvertService.IsGoogleDriveUrl(subscribtionPlanDto.ImageUrl))
            {
                return BadRequest("Invalid image url, only Google Drive urls are allowed.");
            }

            string imageUrl = urlConvertService.ConvertGoogleDriveUrl(subscribtionPlanDto.ImageUrl);

            var subscriptionPlan = dbContext.SubscriptionPlans.Find(id);

            if (subscriptionPlan == null)
            {
                return NotFound();
            }

            subscriptionPlan.Name = subscribtionPlanDto.Name;
            subscriptionPlan.Price = subscribtionPlanDto.Price;
            subscriptionPlan.FreeDelivery = subscribtionPlanDto.FreeDelivery;
            subscriptionPlan.Details = subscribtionPlanDto.Details;
            subscriptionPlan.Savings = subscribtionPlanDto.Savings;
            subscriptionPlan.ImageUrl = imageUrl;

            dbContext.SaveChanges();

            return Ok(subscriptionPlan);
        }

        [HttpDelete]
        [Route("id={id:int}")]
        public IActionResult DeleteSubscriptionPlan(int id)
        {
            var subscriptionPlan = dbContext.SubscriptionPlans.Find(id);

            if (subscriptionPlan == null)
            {
                return NotFound();
            }

            dbContext.SubscriptionPlans.Remove(subscriptionPlan);
            dbContext.SaveChanges();

            return Ok();
        }
    }
}