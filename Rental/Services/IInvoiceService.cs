using Rental.Models;
using System.Collections.Generic;

namespace Rental.Services
{
    public interface IInvoiceService
    {
        CalculatedInvoice GetCalculatedShoppingCart(List<ShoppingCartItem> shoppingCartItems);
    }
}