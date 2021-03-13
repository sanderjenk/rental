using AutoMapper;
using Rental.Entities;
using Rental.Enums;
using Rental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        const decimal ONE_TIME = 100;
        const decimal PREMIUM = 60;
        const decimal REGULAR = 40;

        IRentingRepository _rentingRepository;
        IMapper _mapper;
        public ShoppingCartService(IRentingRepository rentingRepository, IMapper mapper)
        {
            _rentingRepository = rentingRepository;
            _mapper = mapper;
        }

        public CalculatedShoppingCart GetCalculatedShoppingCart(List<ShoppingCartItem> shoppingCartItems)
        {
            var items = shoppingCartItems.Select(x =>
            {
                var equipment = _rentingRepository.GetEquipment(x.EquipmentId);

                var strategyContext = new StrategyContext(equipment, x.Days);

                var strategy = strategyContext.GetStrategy(equipment.Type);

                var price = strategyContext.ApplyStrategy(strategy);

                var equipmentDto = _mapper.Map<EquipmentDto>(equipment);

                return new CalculatedShoppingCartItem
                {
                    Days = x.Days,
                    Equipment = equipmentDto,
                    Price = price
                };
            });

            var totalPrice = items.Sum(x => x.Price);

            return new CalculatedShoppingCart { CartItems = items, TotalPrice = totalPrice };
        }

        class StrategyContext
        {
            int _days;
            Equipment _equipment;
            Dictionary<string, IPricingStrategy> strategyContext
                = new Dictionary<string, IPricingStrategy>();
            public StrategyContext(Equipment equipment, int days)
            {
                _days = days;
                _equipment = equipment;
                strategyContext.Add(nameof(HeavyPricingStrategy),
                        new HeavyPricingStrategy());
                strategyContext.Add(nameof(RegularPricingStrategy),
                        new RegularPricingStrategy());
                strategyContext.Add(nameof(SpecializedPricingStrategy),
                        new SpecializedPricingStrategy());
            }

            public decimal ApplyStrategy(IPricingStrategy strategy)
            {
                return strategy.CalculatePrice(_equipment, _days);
            }

            public IPricingStrategy GetStrategy(EquipmentType equipmentType)
            {
                switch (equipmentType)
                {
                    case EquipmentType.Heavy:
                        return strategyContext[nameof(HeavyPricingStrategy)];
                    case EquipmentType.Regular:
                        return strategyContext[nameof(RegularPricingStrategy)];
                    case EquipmentType.Specialized:
                        return strategyContext[nameof(SpecializedPricingStrategy)];
                    default:
                        return strategyContext[nameof(RegularPricingStrategy)];
                }
            }
        }
        public interface IPricingStrategy
        {
            decimal CalculatePrice(Equipment equipment, int days);
        }
        public class HeavyPricingStrategy : IPricingStrategy
        {

            public decimal CalculatePrice(Equipment equipment, int days)
            {
                return ONE_TIME + days * PREMIUM;
            }
        }

        public class RegularPricingStrategy : IPricingStrategy
        {

            public decimal CalculatePrice(Equipment equipment, int days)
            {
                return 10;
            }
        }

        public class SpecializedPricingStrategy : IPricingStrategy
        {

            public decimal CalculatePrice(Equipment equipment, int days)
            {
                return 1;
            }
        }
    }
}
