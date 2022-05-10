using EcommerceProject.Domain.SeedWork;
using EcommerceProject.Domain.SharedKermel;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EcommerceProject.Domain.Test.SharedKermel
{
    public class MoneyValueTest
    {
        [Fact]
        public void GivenMoneyValueOf_WhenProvidingCurrency_IsSuccessful()
        {
            decimal value = 300;
            var currency = "USA";

            var moneyvalue = MoneyValue.Of(value, currency);

            Assert.Equal(value, moneyvalue.Value);
            Assert.Equal(currency, moneyvalue.Currency);
        }

        [Fact] 
        public void CheckRule_WhenNotProvidingCurrency_ThrowsMoneyValueMustHaveCurrencyRuleBroken()
        {
            var exception = Assert.Throws<BusinessRuleValidationException>(() => MoneyValue.Of(100, null));

            string message = "Money value must have currency";
            Assert.Equal(message, exception.Message);
        }

        [Fact] 
        public void GivenMoneyValue_WhenAddingTwoMoneyValues_IsSuccessful()
        {
            var value1 = MoneyValue.Of(100, "USA");
            var value2 = MoneyValue.Of(200, "USA");

            var sumMoneyValue = value1 + value2;

            Assert.Equal(MoneyValue.Of(300, "USA"), sumMoneyValue);
        }

        [Fact]
        public void CheckRule_WhenAddingTwoValueDifferenceCurrency_ThrowExption()
        {
            var value1 = MoneyValue.Of(100, "USA");
            var value2 = MoneyValue.Of(100, "VND");

            var exception = Assert.Throws<BusinessRuleValidationException>(() => { var sum = value1 + value2; });

            var message = "Money value currencies must be the same";
            Assert.Equal(message, exception.Message);
        }

        [Fact] 
        public void GivenMoneyValue_WhenMultiplyingMoneyValuesWithIntNumber_IsSuccessful()
        {
            var value = MoneyValue.Of(100, "USA");
            var number = 3;

            var multipValue = number * value;

            Assert.Equal(MoneyValue.Of(300, "USA"), multipValue);
        }

        [Fact]
        public void GivenMoneyValue_WhenMultiplyingMoneyValuesWithDecimalNumber_IsSuccessful()
        {
            var value = MoneyValue.Of(100, "USA");
            decimal number = 3;

            var multipValue = number * value;

            Assert.Equal(MoneyValue.Of(300, "USA"), multipValue);
        }

        [Fact]
        public void GivenSumMoneyValueExtentions_WhenAddingTheValueInList_IsSuccessful()
        {
            var value1 = MoneyValue.Of(100, "USA");
            var value2 = MoneyValue.Of(200, "USA");
            var value3 = MoneyValue.Of(500, "USA");
            var list = new List<MoneyValue>() { value1, value2, value3 };

            MoneyValue add = list.Sum();

            var result = MoneyValue.Of(800, "USA");
            Assert.Equal(result.Value, add.Value);
            Assert.Equal(result.Currency, add.Currency);
        }
        
    }
}
