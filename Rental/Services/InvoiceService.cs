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

                var price = strategyContext.ApplyPriceStrategy(strategy);

                var bonusPoints = strategyContext.ApplyBonusPointsStrategy(strategy);

                var equipmentDto = _mapper.Map<EquipmentDto>(equipment);

                return new InvoiceLine
                {
                    Days = x.Days,
                    Equipment = equipmentDto,
                    Price = price,
                    BonusPoints = bonusPoints,
                };
            });

            var totalbonusPoints = items.Sum(x => x.BonusPoints);

            var totalPrice = items.Sum(x => x.Price);

            return new CalculatedInvoice
            {
                InvoiceLines = items,
                TotalPrice = totalPrice,
                TotalBonusPoints = totalbonusPoints
            };
        }

        public class StrategyContext
        {
            int _days;
            Equipment _equipment;
            Dictionary<string, IPricingStrategy> strategyContext
                = new Dictionary<string, IPricingStrategy>();
            public StrategyContext(Equipment equipment, int days)
            {
                _days = days;

                if (days < 1)
                {
                    throw new ArgumentException($"{nameof(days)} has to be greater than 0");
                }

                _equipment = equipment ?? throw new ArgumentNullException(nameof(equipment));
                strategyContext.Add(nameof(HeavyPricingStrategy),
                        new HeavyPricingStrategy());
                strategyContext.Add(nameof(RegularPricingStrategy),
                        new RegularPricingStrategy());
                strategyContext.Add(nameof(SpecializedPricingStrategy),
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
            int CalculateBonusPoints();
        }
        public class HeavyPricingStrategy : IPricingStrategy
        {
            public int CalculateBonusPoints() => 2;


            public decimal CalculatePrice(Equipment equipment, int days)
            {
                return ONE_TIME + days * PREMIUM;
            }
        }

        public class RegularPricingStrategy : IPricingStrategy
        {
            public int CalculateBonusPoints() => 1;

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
            public int CalculateBonusPoints() => 1;

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
