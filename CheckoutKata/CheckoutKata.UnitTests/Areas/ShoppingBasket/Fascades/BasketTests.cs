using System.Collections.Generic;
using CheckoutKata.Areas.ShoppingBasket.Fascades;
using CheckoutKata.Areas.Stock.Models;
using CheckoutKata.Areas.Stock.Repositories;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace CheckoutKata.UnitTests.Areas.ShoppingBasket.Fascades
{
    public class BasketTests
    {
        #region Fields

        private readonly IBasket _sut;
        private readonly IItemRepository _itemRepository;

        #endregion

        #region Constructor

        public BasketTests()
        {
            _itemRepository = Substitute.For<IItemRepository>();

            foreach (var sku in "ABCD")
            {
                var item = Substitute.For<IItem>();
                item.Sku.Returns(sku);
                _itemRepository.GetItemBySku(sku).Returns(item);
            }

            _sut = new Basket(_itemRepository);
        }

        #endregion

        #region Tests

        [Theory]
        [InlineData('A')]
        [InlineData('B')]
        [InlineData('C')]
        [InlineData('D')]
        public void AddItem_ValidSkuProvided_ReturnsTrue(char sku)
        {
            // Act
            var result = _sut.AddItem(sku);

            // Assert
            result.Should().BeTrue();
        }

        #endregion
    }
}
