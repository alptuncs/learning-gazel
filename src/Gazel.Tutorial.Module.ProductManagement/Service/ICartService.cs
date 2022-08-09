namespace Gazel.Tutorial.Module.ProductManagement.Service
{
    public interface ICartInfo
    {
        int Id { get; }
        string UserName { get; }
        Money TotalCost { get; }
        bool PurchaseComplete { get; }
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
        ICartInfo GetCart(Cart cart);
        List<ICartInfo> GetNonEmptyCarts();
        ICartInfo GetCartWithName(string name);
    }
}
