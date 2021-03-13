using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.Models
{
    public class InvoiceLine
    {
        public EquipmentDto Equipment { get; set; }
        public int Days { get; set; }
        public decimal Price { get; set; }
        public int BonusPoints { get; set; }
    }
}
