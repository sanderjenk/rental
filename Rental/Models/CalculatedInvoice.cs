using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.Models
{
    public class CalculatedInvoice
    {
        public IEnumerable<InvoiceLine> CartItems { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalBonusPoints { get; set; }
    }
}
