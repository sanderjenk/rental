using Rental.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Rental.Services.PricingStrategies;
using Xunit;

namespace RentalTest
{
    public class PriceCalculationTest
    {
        [Fact]
        public void ExceptionThrowingNullEquipmentTest()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new StrategyContext(null, 5));

            Assert.Equal("Value cannot be null. (Parameter 'equipment')", ex.Message);
        }

        [Fact]
        public void ExceptionThrowingNegativeDaysTest()
        {
            var equipment = new Equipment
            {
                Name = "TestEquipment",
                Type = Rental.Enums.EquipmentType.Heavy,
                Id = 1
            }; 
            
            var ex = Assert.Throws<ArgumentException>(() => new StrategyContext(equipment, -2));

            Assert.Equal("days has to be greater than 0", ex.Message);
        }

        [Fact]
        public void PassingHeavyTest()
        {
            var equipment = new Equipment
            {
                Name = "TestEquipment",
                Type = Rental.Enums.EquipmentType.Heavy,
                Id = 1
            };

            var strategyContext = new StrategyContext(equipment, 5);
            var priceStrategy = strategyContext.GetStrategy(equipment.Type);
            var price = strategyContext.ApplyPriceStrategy(priceStrategy);
            var bonusPoints = strategyContext.ApplyBonusPointsStrategy(priceStrategy);
            Assert.Equal(400, price);
            Assert.Equal(2, bonusPoints);
        }

        [Fact]
        public void PassingRegularLessThan2DaysTest()
        {
            var equipment = new Equipment
            {
                Name = "TestEquipment",
                Type = Rental.Enums.EquipmentType.Regular,
                Id = 1
            };

            var strategyContext = new StrategyContext(equipment, 2);
            var priceStrategy = strategyContext.GetStrategy(equipment.Type);
            var price = strategyContext.ApplyPriceStrategy(priceStrategy);
            var bonusPoints = strategyContext.ApplyBonusPointsStrategy(priceStrategy);

            Assert.Equal(220, price);
            Assert.Equal(1, bonusPoints);
        }

        [Fact]
        public void PassingRegularMoreThan2DaysTest()
        {
            var equipment = new Equipment
            {
                Name = "TestEquipment",
                Type = Rental.Enums.EquipmentType.Regular,
                Id = 1
            };

            var strategyContext = new StrategyContext(equipment, 10);
            var priceStrategy = strategyContext.GetStrategy(equipment.Type);
            var price = strategyContext.ApplyPriceStrategy(priceStrategy);
            var bonusPoints = strategyContext.ApplyBonusPointsStrategy(priceStrategy);

            Assert.Equal(540, price);
            Assert.Equal(1, bonusPoints);
        }

        [Fact]
        public void PassingSpecializedMoreThan3DaysTest()
        {
            var equipment = new Equipment
            {
                Name = "TestEquipment",
                Type = Rental.Enums.EquipmentType.Specialized,
                Id = 1
            };

            var strategyContext = new StrategyContext(equipment, 4);
            var priceStrategy = strategyContext.GetStrategy(equipment.Type);
            var price = strategyContext.ApplyPriceStrategy(priceStrategy);
            var bonusPoints = strategyContext.ApplyBonusPointsStrategy(priceStrategy);

            Assert.Equal(220, price);
            Assert.Equal(1, bonusPoints);
        }

        [Fact]
        public void PassingSpecializedLessThan3DaysTest()
        {
            var equipment = new Equipment
            {
                Name = "TestEquipment",
                Type = Rental.Enums.EquipmentType.Specialized,
                Id = 1
            };

            var strategyContext = new StrategyContext(equipment, 2);
            var priceStrategy = strategyContext.GetStrategy(equipment.Type);
            var price = strategyContext.ApplyPriceStrategy(priceStrategy);
            var bonusPoints = strategyContext.ApplyBonusPointsStrategy(priceStrategy);

            Assert.Equal(120, price);
            Assert.Equal(1, bonusPoints);
        }
    }
}
