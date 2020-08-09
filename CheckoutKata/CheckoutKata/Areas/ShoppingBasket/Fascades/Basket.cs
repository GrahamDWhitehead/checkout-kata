using System.Collections.Generic;
using CheckoutKata.Areas.Stock.Models;
using CheckoutKata.Areas.Stock.Repositories;

namespace CheckoutKata.Areas.ShoppingBasket.Fascades
{
    public class Basket : IBasket
    {
        #region Fields

        private readonly ICollection<IItem> _basketItems = new List<IItem>();
        private readonly IItemRepository _itemRepository;

        #endregion

        #region Constructor

        public Basket(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        #endregion

        #region IBasket

        public bool AddItem(char sku)
        {
            var item = _itemRepository.GetItemBySku(sku);
            if (item == null) return false;

            _basketItems.Add(item);
            return true;
        }

        #endregion
    }
}
