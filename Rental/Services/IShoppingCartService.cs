using Rental.Models;
using System.Collections.Generic;

namespace Rental.Services
{
    public interface IShoppingCartService
    {
        CalculatedShoppingCart GetCalculatedShoppingCart(List<ShoppingCartItem> shoppingCartItems);
    }
}