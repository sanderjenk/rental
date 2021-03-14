using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rental.Entities;

namespace Rental.Services.PricingStrategies
{
    public class SpecializedPricingStrategy : IPricingStrategy
    {
        public int CalculateBonusPoints() => 1;

        public decimal CalculatePrice(Equipment equipment, int days)
        {
            decimal price = 0;
            var counter = 1;
            for (var i = 0; i < days; i++)
            {
                if (counter <= 3)
                {
                    price += Prices.Premium;
                }
                else
                {
                    price += Prices.Regular;
                }
                counter++;
            }
            return price;
        }
    }
}
