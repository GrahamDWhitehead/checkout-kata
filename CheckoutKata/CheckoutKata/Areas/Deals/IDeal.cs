using System.Collections.Generic;
using CheckoutKata.Areas.Stock.Models;

namespace CheckoutKata.Areas.Deals
{
    public interface IDeal
    {
        IItem ItemOnOffer { get; set; }
        decimal CalculateApplicableDiscount(ICollection<IItem> basketItems);
    }
}
