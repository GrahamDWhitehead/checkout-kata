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
        private readonly IDeal _xItemsForYDeal;

        #endregion

        #region Constructor

        public BasketTests()
        {
            _itemRepository = Substitute.For<IItemRepository>();

            var inventory = new Dictionary<char, decimal>
            {
                {'A', 10},
                {'B', 15},
                {'C', 40},
                {'D', 55}
            };
            foreach (var inventoryItem in inventory)
            {
                var item = Substitute.For<IItem>();
                item.Sku.Returns(inventoryItem.Key);
                item.UnitPrice.Returns(inventoryItem.Value);
                _itemRepository.GetItemBySku(inventoryItem.Key).Returns(item);
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
        [InlineData('A', 10)]
        [InlineData('B', 15)]
        [InlineData('C', 40)]
        [InlineData('D', 55)]
        public void AddItem_ValidSkuProvided_ItemPriceAddedToBasketTotal(char sku, decimal expectedTotal)
        {
            // Act
            _sut.AddItem(sku);

            // Assert
            _sut.Total.Should().Be(expectedTotal);
        }

        [Theory]
        [InlineData('A', 10)]
        [InlineData('B', 15)]
        [InlineData('C', 40)]
        [InlineData('D', 55)]
        public void AddItem_ValidSkuProvided_DiscountsApplied(char sku, decimal expectedTotal)
        {
            // Act
            _sut.AddItem(sku);

            // Assert
            _xItemsForYDeal.Received(1).CalculateApplicableDiscount(Arg.Is<List<IItem>>(x
                => x.Count.Equals(1)));
        }

        #endregion
    }
}
