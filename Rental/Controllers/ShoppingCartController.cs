using Microsoft.AspNetCore.Mvc;
using Rental.Models;
using Rental.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rental.Controllers
{
    [Route("api/shoppingcart")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpPost]
        public IActionResult GetShoppingCart(List<ShoppingCartItem> shoppingCartItems)
        {
            var shoppingCart = _shoppingCartService.GetCalculatedShoppingCart(shoppingCartItems);
            return Ok(shoppingCart);
        }
    }
}
