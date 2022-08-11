namespace Gazel.Tutorial.Module.ProductManagement.Service
{
    public interface IProductInfo
    {
        int Id { get; }
        string Name { get; }
        Money Price { get; }
        int Stock { get; }
    }

    public interface IProductService
    {
        Product RevisePrice(Money price = default);
        void MakeUnavailable();
    }

    public interface IProductsService
    {
        IProductInfo GetProduct(Product product);
        List<IProductInfo> GetProductsWithPositiveStock();
        List<IProductInfo> GetProductsWithName(string name);
        List<IProductInfo> GetProductsWithinPriceRange(MoneyRange range);
    }

    public interface IProductManagerService
    {
        Product CreateProduct(string name, Money price, int stock);
        Cart CreateCart(string userName);
    }
}
