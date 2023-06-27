using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class BasketController : BaseAPIController
    {
        private readonly StoreContext _context;

        public BasketController(StoreContext context)
        {
            _context = context;
        }
        
        [HttpGet]

        public async Task<ActionResult<BasketDto>> GetBasket()
        {
            var basket = await RetriveBaskets();

            if (basket == null) return NotFound();

            return new BasketDto 
            {
                Id = basket.Id,
                buyerId = basket.BuyerId,
                Items = basket.Items.Select(item => new BasketItemDto 
                {
                    Id = item.ProductId,
                    Name = item.Product.Name,
                    Price = item.Product.Price,
                    PictureUrl = item.Product.PictureUrl,
                    Type = item.Product.Type,
                    QuantityInStock = item.Quanity
                }).ToList()
            };
        }

        

        [HttpPost] // api/basket?productId=3&quantity=2

        public async Task<ActionResult<basket>> AddItemtoBasket(int productId, int quanity)
        {
            // create a basket

            var basket = await RetriveBaskets();
            if(basket == null) basket = CreateBasket();

            // get the product

            var product = await _context.Products.FindAsync(productId);
            if(product == null) return NotFound();

            // add item

            basket.addItem(product, quanity);

            // save changes

            var result = await _context.SaveChangesAsync() > 0;

            if(result) return StatusCode(201);

            return BadRequest(new ProblemDetails{Title = "Problem saving item to basket"});
        }

        [HttpDelete]

        public async Task<ActionResult<basket>> DeleteItemFromBasket(int productId, int quanity)
        {
            var basket = await RetriveBaskets(); // get the basket
            if(basket == null ) return NotFound();

            basket.RemoveItem(productId, quanity); // remove method

            var result = await _context.SaveChangesAsync() > 0;
            if (result) return Ok(); // save changes

            return BadRequest(new ProblemDetails{Title = "Problem removing"});
        }

        private async Task<basket?> RetriveBaskets()
        {
            return await _context.Baskets
                        .Include(i => i.Items)
                        .ThenInclude(p => p.Product)
                        .FirstOrDefaultAsync(x => x.BuyerId == Request.Cookies["buyerId"]);
        }

        private basket CreateBasket()
        {
            var buyerId = Guid.NewGuid().ToString();
            var cookiesOptions = new CookieOptions{IsEssential = true, Expires = DateTime.Now.AddDays(30)}; 
            Response.Cookies.Append("buyerId", buyerId, cookiesOptions);
            var basket = new basket{BuyerId = buyerId};
            _context.Baskets.Add(basket);

            return basket;
        }


    }
}