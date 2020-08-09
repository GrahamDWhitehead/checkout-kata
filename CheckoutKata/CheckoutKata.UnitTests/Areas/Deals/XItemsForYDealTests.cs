using System.Collections.Generic;
using CheckoutKata.Areas.Stock.Models;
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

        [Fact]
        public void CalculateApplicableDiscount_3For40()
        {
            // Arrange
            var item = Substitute.For<IItem>();
            item.UnitPrice.Returns(15);
            item.Sku.Returns('B');

            var basket = new List<IItem>
            {
                item, item, item
            };

            _sut.ItemOnOffer = item;
            _sut.DiscountGiven = 5;
            _sut.ItemsToQualify = 3;

            // Act
            var result = _sut.CalculateApplicableDiscount(basket);

            // Assert
            result.Should().Be(5);
        }

        #endregion
    }
}
