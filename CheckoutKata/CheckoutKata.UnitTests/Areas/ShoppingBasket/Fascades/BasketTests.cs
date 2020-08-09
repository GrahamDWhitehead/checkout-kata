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

        [Theory]
        [InlineData('a')]
        [InlineData('X')]
        [InlineData(' ')]
        [InlineData('1')]
        public void AddItem_InvalidSkuProvided_ReturnsFalse(char sku)
        {
            // Arrange
            _itemRepository.GetItemBySku(sku).Returns((IItem)null);

            // Act
            var result = _sut.AddItem(sku);

            // Assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData('a', 10)]
        [InlineData('b', 15)]
        [InlineData('c', 40)]
        [InlineData('d', 55)]
        public void AddItem_ValidSkuProvided_ItemPriceAddedToBasketTotal(char sku, decimal expectedTotal)
        {
            // Act
            _sut.AddItem(sku);

            // Assert
            _sut.Total.Should().Be(expectedTotal);
        }

        #endregion
    }
}
