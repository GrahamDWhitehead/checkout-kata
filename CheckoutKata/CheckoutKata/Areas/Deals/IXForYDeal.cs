
namespace CheckoutKata.Areas.Deals
{
    public interface IXForYDeal : IDeal
    {
        int ItemsToQualify { get; set; }
        decimal DiscountGiven { get; set; }
    }
}
