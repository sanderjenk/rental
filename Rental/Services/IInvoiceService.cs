using Rental.Models;
using System.Collections.Generic;

namespace Rental.Services
{
    public interface IInvoiceService
    {
        CalculatedInvoice GetCalculatedInvoice(List<ShoppingCartItem> shoppingCartItems);
    }
}