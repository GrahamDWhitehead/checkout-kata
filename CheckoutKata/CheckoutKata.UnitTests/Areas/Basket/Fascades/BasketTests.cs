using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CheckoutKata.UnitTests.Areas.Basket.Fascades
{
    public class BasketTests
    {
        #region Fields

        private IBasket _sut;

        #endregion

        #region Constructor

        public BasketTests()
        {
            _sut = new Basket();
        }

        #endregion

        #region Tests

        [Theory]
        [InlineData('A')]
        [InlineData('B')]
        [InlineData('C')]
        [InlineData('D')]
        public void AddItem_ValidSkyProvided_ReturnsTrue(char sku)
        {
            // Act
            var result = _sut.AddItem(sku);

            // Assert
            result.Should().BeTrue();
        }

        #endregion
    }
}
