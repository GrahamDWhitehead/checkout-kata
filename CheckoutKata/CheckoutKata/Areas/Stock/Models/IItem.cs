namespace CheckoutKata.Areas.Stock.Models
{
    public interface IItem
    {
        char Sku { get; }
        decimal UnitPrice { get; }
    }
}
