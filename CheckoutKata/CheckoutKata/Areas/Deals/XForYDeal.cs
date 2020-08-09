using System.Collections.Generic;
using CheckoutKata.Areas.Stock.Models;
using System.Linq;

namespace CheckoutKata.Areas.Deals
{
    public class XForYDeal : IXForYDeal
    {
        #region IXForYDeal

        public int ItemsToQualify { get; set; }
        public decimal DiscountGiven { get; set; }

        #endregion

        #region IDeal

        public IItem ItemOnOffer { get; set; }

        public decimal CalculateApplicableDiscount(ICollection<IItem> basketItems)
        {
            var quantityInBasket = basketItems.Count(x => x.Sku.Equals(ItemOnOffer.Sku));
            var multiplesOfDeal = quantityInBasket / ItemsToQualify;
            return DiscountGiven * multiplesOfDeal;
        }

        #endregion
    }
}
