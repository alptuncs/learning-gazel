namespace Gazel.Tutorial.Module.ProductManagement.Service
{
    public interface ICartInfo
    {
        int Id { get; }
        string UserName { get; }
        float TotalCost { get; }
    }

    public interface ICartService
    {
        void AddToCart(Product product, int amount);
        void AddToCart(Product product);
        void RemoveFromCart(Product product);
        void RemoveAllProducts();
        void DeleteCart();
    }
    public interface ICartsService
    {
        ICartInfo GetCart(Cart cart);
        List<ICartInfo> GetNonEmptyCarts();
        ICartInfo GetCartWithName(string name);
    }
}
