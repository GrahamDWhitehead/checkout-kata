using System.Collections.Generic;
using CheckoutKata.Areas.Deals;
using CheckoutKata.Areas.Stock.Models;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace CheckoutKata.UnitTests.Areas.Deals
{
    public class XItemsForYDealTests
    {
        #region Fields

        private readonly IXForYDeal _sut;

        #endregion

        #region Constructor

        public XItemsForYDealTests()
        {
            _sut = new XForYDeal();
        }

        #endregion

        #region Tests

        [Theory]
        [InlineData(5, 3, 5)]
        [InlineData(10, 2, 20)]
        [InlineData(10, 4, 10)]
        public void CalculateApplicableDiscount_3For40(decimal discountGiven, int itemsToQualify, decimal expectedDiscount)
        {
            // Arrange
            var item = Substitute.For<IItem>();
            item.UnitPrice.Returns(15);
            item.Sku.Returns('B');

            var basket = new List<IItem>
            {
                item, item, item, item, item
            };

            _sut.ItemOnOffer = item;
            _sut.DiscountGiven = discountGiven;
            _sut.ItemsToQualify = itemsToQualify;

            // Act
            var result = _sut.CalculateApplicableDiscount(basket);

            // Assert
            result.Should().Be(expectedDiscount);
        }

        #endregion
    }
}
