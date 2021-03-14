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
    [Route("api/invoices")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost]
        public IActionResult GetCalculatedInvoice(List<ShoppingCartItem> shoppingCartItems)
        {
            var shoppingCart = _invoiceService.GetCalculatedInvoice(shoppingCartItems);
            return Ok(shoppingCart);
        }
    }
}
