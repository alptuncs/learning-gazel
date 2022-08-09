namespace Gazel.Tutorial.Module.ProductManagement.Service
{
    public interface ICartInfo
    {
        int Id { get; }
        string UserName { get; }
        float TotalCost { get; }
        bool PurchaseComplete { get; }
    }

    public interface ICartService
    {
        void AddToCart(Product product, int amount);
        void RemoveFromCart(Product product);
        void RemoveAllProducts();
        PurchaseRecord CompletePurchase();

    }
    public interface ICartsService
    {
        ICartInfo GetCart(Cart cart);
        List<ICartInfo> GetNonEmptyCarts();
        ICartInfo GetCartWithName(string name);
    }
}
