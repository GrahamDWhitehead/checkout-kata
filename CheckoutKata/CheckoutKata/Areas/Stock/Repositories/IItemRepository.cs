using CheckoutKata.Areas.Stock.Models;

namespace CheckoutKata.Areas.Stock.Repositories
{
    public interface IItemRepository
    {
        IItem GetItemBySku(char sku);
    }
}
