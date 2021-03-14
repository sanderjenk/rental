using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rental.Entities;

namespace Rental.Services.PricingStrategies
{
    public interface IPricingStrategy
    {
        decimal CalculatePrice(Equipment equipment, int days);
        int CalculateBonusPoints();
    }
}
