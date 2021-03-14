using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rental.Entities;
using Rental.Enums;

namespace Rental.Services.PricingStrategies
{
    public class StrategyContext
    {
        private readonly int _days;
        private readonly Equipment _equipment;
        private readonly Dictionary<string, IPricingStrategy> _strategyContext
            = new Dictionary<string, IPricingStrategy>();
        public StrategyContext(Equipment equipment, int days)
        {
            _days = days;

            if (days < 1)
            {
                throw new ArgumentException($"{nameof(days)} has to be greater than 0");
            }

            _equipment = equipment ?? throw new ArgumentNullException(nameof(equipment));
            _strategyContext.Add(nameof(HeavyPricingStrategy),
                new HeavyPricingStrategy());
            _strategyContext.Add(nameof(RegularPricingStrategy),
                new RegularPricingStrategy());
            _strategyContext.Add(nameof(SpecializedPricingStrategy),
                new SpecializedPricingStrategy());
        }

        public decimal ApplyPriceStrategy(IPricingStrategy strategy)
        {
            return strategy.CalculatePrice(_equipment, _days);
        }

        public int ApplyBonusPointsStrategy(IPricingStrategy strategy)
        {
            return strategy.CalculateBonusPoints();
        }

        public IPricingStrategy GetStrategy(EquipmentType equipmentType)
        {
            return equipmentType switch
            {
                EquipmentType.Heavy => _strategyContext[nameof(HeavyPricingStrategy)],
                EquipmentType.Regular => _strategyContext[nameof(RegularPricingStrategy)],
                EquipmentType.Specialized => _strategyContext[nameof(SpecializedPricingStrategy)],
                _ => _strategyContext[nameof(RegularPricingStrategy)]
            };
        }
    }
}
