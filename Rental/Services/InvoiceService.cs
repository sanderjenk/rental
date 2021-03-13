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
    public class InvoiceService : IInvoiceService
    {
        const decimal ONE_TIME = 100;
        const decimal PREMIUM = 60;
        const decimal REGULAR = 40;

        IRentingRepository _rentingRepository;
        IMapper _mapper;
        public InvoiceService(IRentingRepository rentingRepository, IMapper mapper)
        {
            _rentingRepository = rentingRepository;
            _mapper = mapper;
        }

        public CalculatedInvoice GetCalculatedShoppingCart(List<ShoppingCartItem> shoppingCartItems)
        {
            var items = shoppingCartItems.Select(x =>
            {
                var equipment = _rentingRepository.GetEquipment(x.EquipmentId);

                var strategyContext = new StrategyContext(equipment, x.Days);

                var strategy = strategyContext.GetStrategy(equipment.Type);

                var price = strategyContext.ApplyStrategy(strategy);

                var equipmentDto = _mapper.Map<EquipmentDto>(equipment);

                return new InvoiceLine
                {
                    Days = x.Days,
                    Equipment = equipmentDto,
                    Price = price
                };
            });

            var totalPrice = items.Sum(x => x.Price);

            return new CalculatedInvoice { CartItems = items, TotalPrice = totalPrice };
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
                var price = ONE_TIME;
                var counter = 1;
                for (int i = 0; i < days; i++)
                {
                    if (counter <= 2)
                    {
                        price += PREMIUM;
                    }
                    else
                    {
                        price += REGULAR;
                    }
                    counter++;
                }
                return price;
            }
        }

        public class SpecializedPricingStrategy : IPricingStrategy
        {

            public decimal CalculatePrice(Equipment equipment, int days)
            {
                decimal price = 0;
                var counter = 1;
                for (int i = 0; i < days; i++)
                {
                    if (counter <= 3)
                    {
                        price += PREMIUM;
                    }
                    else
                    {
                        price += REGULAR;
                    }
                    counter++;
                }
                return price;
            }
        }
    }
}
