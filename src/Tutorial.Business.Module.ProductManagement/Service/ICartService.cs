namespace Tutorial.Business.Module.ProductManagement.Service
{
    public interface ICartInfo
    {
        int Id { get; }
        string UserName { get; }
        Money TotalCost { get; }
        bool PurchaseComplete { get; }
    }

    public interface ICartDetail
    {
        int Id { get; }
        string UserName { get; }
        Money TotalCost { get; }
        bool PurchaseComplete { get; }
        List<ICartItemInfo> Items { get; }
    }

    public interface ICartItemInfo
    {
        int Id { get; }
        int Amount { get; }
        IGenericInfo Product { get; }
    }

    public interface ICartService
    {
        void AddProduct(Product product, int amount);
        void RemoveProduct(Product product);
        void RemoveAllProducts();
        PurchaseRecord Purchase();

    }

    public interface ICartsService
    {
        ICartDetail GetCart(int cartId);
        List<ICartInfo> GetCarts(string userName);
    }

    public interface ICartManagerService
    {
        ICartDetail CreateCart(string userName);
    }
}
