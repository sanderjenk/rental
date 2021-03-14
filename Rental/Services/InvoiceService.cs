using System;
using AutoMapper;
using Rental.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rental.Services.PricingStrategies;

namespace Rental.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IRentingRepository _rentingRepository;
        private readonly IMapper _mapper;
        public InvoiceService(IRentingRepository rentingRepository, IMapper mapper)
        {
            _rentingRepository = rentingRepository;
            _mapper = mapper;
        }

        public CalculatedInvoice GetCalculatedInvoice(List<ShoppingCartItem> shoppingCartItems)
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
            }).ToList();

            var totalBonusPoints = items.Sum(x => x.BonusPoints);

            var totalPrice = items.Sum(x => x.Price);

            return new CalculatedInvoice
            {
                InvoiceLines = items,
                TotalPrice = totalPrice,
                TotalBonusPoints = totalBonusPoints
            };
        }
    }
}
