using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rental.Entities;

namespace Rental.Services.PricingStrategies
{
    public class HeavyPricingStrategy : IPricingStrategy
    {
        public int CalculateBonusPoints() => 2;


        public decimal CalculatePrice(Equipment equipment, int days)
        {
            return Prices.OneTime + days * Prices.Premium;
        }
    }
}
